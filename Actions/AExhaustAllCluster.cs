using JohannaTheTrucker.MidrowStuff;
using JohannaTheTrucker.SpecialEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Actions
{
    public class AExhaustAllCluster : CardAction
    {

        public double target_runtime;
        public double fire_rate;

        public List<Tuple<ClusterMissile, double, int>> future_hits = new List<Tuple<ClusterMissile, double, int>>();

        private const double flight_time = 0.25;

        public bool fire_only_once;
        public bool fired_first_salvo;
        public bool fromPlayer;

        public override void Begin(G g, State s, Combat c)
        {
            //pick target_runtime based on largest cluster
            var largest_cluster = c.stuff.Values.Max(stuff => (stuff as ClusterMissile)?.stackSize ?? 0);
            if (largest_cluster <= 5)
            {
                target_runtime = 0.75;
            }
            else if (largest_cluster <= 15)
            {
                target_runtime = 1.5;

            }
            else
            {
                target_runtime = 3;
            }
            timer = target_runtime + flight_time;
            fire_rate = largest_cluster / (target_runtime);

        }

        public override bool CanSkipTimerIfLastEvent() => false;

        public double fire_leftover;

        private static Status smart_explosive_status = (Status)(Manifest.SmartExplosiveStatus?.Id ?? throw new Exception("Missing smart explosive status"));

        public bool last_frame;

        private int GetSeekerImpact(State s, Combat c, ClusterMissile missile, int world_pos_x)
        {
            if (missile == null)
                return -1;
            int worldX = world_pos_x;

            Ship ship = missile.targetPlayer ? s.ship : c.otherShip;
            int num1 = 99;
            int num2 = 0;
            for (int index = 0; index < ship.parts.Count; ++index)
            {
                if (ship.parts[index].type != PType.empty)
                {
                    num2 = index;
                    if (index < num1)
                        num1 = index;
                }
            }
            if (world_pos_x < ship.x + num1)
                worldX = ship.x + num1;
            if (world_pos_x > ship.x + num2)
                worldX = ship.x + num2;
            if (world_pos_x == worldX)
            {
                Part? partAtWorldX = ship.GetPartAtWorldX(worldX);
                if (partAtWorldX != null && partAtWorldX.type == PType.empty)
                {
                    int num3 = worldX - ship.x;
                    int num4 = 99;
                    int num5 = 0;
                    for (int index = 0; index < ship.parts.Count - 1; ++index)
                    {
                        if (ship.parts[index].type != PType.empty && Math.Abs(num3 - index) < num4)
                        {
                            num5 = index;
                            num4 = Math.Abs(num3 - index);
                        }
                    }
                    worldX = ship.x + num5;
                }
            }
            return worldX;
        }

        public override void Update(G g, State s, Combat c)
        {
            timer -= g.dt;
            if (timer <= 0)
            {
                if (!last_frame)
                {
                    last_frame = true;
                    timer = 1;
                }
                else
                {
                    timer = -1;
                }
            }
            var fired = g.dt * fire_rate + fire_leftover;
            fire_leftover = fired - Math.Truncate(fired);
            fired = Math.Truncate(fired);

            //setup new wave of missiles based on fire value
            bool fired_once = false;
            for (int i = 0; (i < fired) || last_frame; i++)
            {
                if (fired_first_salvo && fire_only_once)
                    break;
                fired_once = false;
                fired_first_salvo = true;
                //Find all cluster
                var all_clusters_entries = c.stuff.Where(e => e.Value is ClusterMissile).Select(e => (e.Key, cluster: e.Value as ClusterMissile)).ToArray();
                foreach (var entry in all_clusters_entries)
                {
                    if (entry.cluster == null)
                        continue;

                    var target_ship = entry.cluster.targetPlayer ? s.ship : c.otherShip;
                    var owner_ship = entry.cluster.fromPlayer ? s.ship : c.otherShip;
                    //only fire personal clusters
                    if (entry.cluster.fromPlayer != fromPlayer)
                        continue;
                    //check if hit missile can hit courtesy of smart explosives
                    var will_hit = entry.cluster.stackType == ClusterMissile.MissileType.seeker || entry.cluster.stackType == ClusterMissile.MissileType.heavy_seeker || target_ship.HasNonEmptyPartAtWorldX(entry.cluster.x);
                    if (owner_ship.Get(smart_explosive_status) > 0)
                        continue;
                    fired_once = true;
                    //detemrine target x
                    var target_x = target_ship.HasNonEmptyPartAtWorldX(entry.cluster.x) ? entry.cluster.x : GetSeekerImpact(s, c, entry.cluster, entry.cluster.x);

                    //get texture
                    var tex = SpriteLoader.Get(entry.cluster.GetIcon() ?? throw new Exception("Missing sprite in cluster missile."));
                    //setup fx
                    c.fx.Add(new FlightFX()
                    {
                        miss = !will_hit,
                        target_flight_time = flight_time,
                        start_x = entry.cluster.x,
                        start_y = FlightFX.YRow.midrow,
                        target_x = target_x,
                        target_y = entry.cluster.targetPlayer ? FlightFX.YRow.player : FlightFX.YRow.enemy,
                        texture = tex,
                    });


                    if (will_hit)
                    {
                        //put cluster in future time
                        future_hits.Add(new(entry.cluster, g.time + flight_time, target_x));
                    }
                    //reduce cluster size by 1
                    entry.cluster.stackSize--;
                    //if cluster size is 0 remove from stuff (item reference is stored in future hits.)
                    if (entry.cluster.stackSize <= 0)
                        c.stuff.Remove(entry.Key);
                }
                //smart exploisive safety on last frame.
                if (!fired_once)
                    break;

            }

            //check which cluster missile has hit.
            var unresolved_hits = future_hits.Where(e => last_frame || e.Item2 <= g.time).ToArray();
            foreach (var hit in unresolved_hits)
            {

                int incomingDamage = hit.Item1.RawDamage();
                foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                    incomingDamage += enumerateAllArtifact.ModifyBaseMissileDamage(s, s.route as Combat, hit.Item1.targetPlayer);

                if (incomingDamage < 0)
                    incomingDamage = 0;
                var target = hit.Item1.targetPlayer ? s.ship : c.otherShip;
                //apply damage/effects
                DamageDone dmg = target.NormalDamage(s, c, incomingDamage, hit.Item3);
                //spawn FX
                EffectSpawner.NonCannonHit(g, hit.Item1.targetPlayer, new RaycastResult() { fromDrone = true, hitDrone = false, hitShip = true, worldX = hit.Item3 }, dmg);

                Part? partAtWorldX = target.GetPartAtWorldX(hit.Item3);

                if ((partAtWorldX != null ? (partAtWorldX.stunnable ? 1 : 0) : 0) != 0)
                    c.QueueImmediate((CardAction)new AStunPart()
                    {
                        worldX = hit.Item3
                    });

                if (target.Get(Status.payback) > 0 || target.Get(Status.tempPayback) > 0)
                {
                    c.QueueImmediate((CardAction)new AAttack()
                    {
                        damage = Card.GetActualDamage(s, target.Get(Status.payback) + target.Get(Status.tempPayback), !hit.Item1.targetPlayer),
                        targetPlayer = !hit.Item1.targetPlayer,
                        fast = true,
                    });
                }
                //clean from list
                future_hits.Remove(hit);
            }
            //stop actrion if we are done
            if (fired >= 1 && !fired_once && future_hits.Count() == 0)
            {
                last_frame = true;
                timer = -1;
            }

        }



    }
}
