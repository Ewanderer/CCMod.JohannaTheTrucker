using JohannaTheTrucker.MidrowStuff;
using JohannaTheTrucker.SpecialEffects;

namespace JohannaTheTrucker.Actions
{
    public class AClusterAttack : CardAction
    {
        public bool done = false;
        public bool will_hit = false;
        public int world_pos_x;

        private ClusterMissile? missile;
        private FlightFX? missile_fx;
        private int target_pos;

        public override void Begin(G g, State s, Combat c)
        {
            if (!c.stuff.TryGetValue(world_pos_x, out var stuff) || stuff is not ClusterMissile msl)
                return;
            this.missile = msl;
            var target = missile.targetPlayer ? s.ship : c.otherShip;

            target_pos = world_pos_x;
            if (missile.stackType == ClusterMissile.MissileType.seeker || missile.stackType == ClusterMissile.MissileType.heavy_seeker)
            {
                will_hit = true;
                //find closest part to target
                if (!target.HasNonEmptyPartAtWorldX(world_pos_x))
                {
                    //find closest part
                    var target_part = target.parts.OrderBy(e => Math.Abs(world_pos_x - target.x + target.parts.IndexOf(e))).FirstOrDefault(e => e.type != PType.empty);
                    if (target_part == null)
                    {
                        target_pos = target.x;
                        will_hit = false;
                    }
                    else
                    {
                        target_pos = target.parts.IndexOf(target_part) + target.x;
                    }
                }
            }
            else
            {
                //check if can hit
                will_hit = target.HasNonEmptyPartAtWorldX(world_pos_x);
            }
            //create fx

            missile_fx = new FlightFX();
            missile_fx.miss = !will_hit;

            missile_fx.texture = SpriteLoader.Get(missile.GetIcon() ?? Spr.icons_recycle);

            missile_fx.start_x = world_pos_x;

            missile_fx.target_x = target_pos;

            missile_fx.start_y = FlightFX.YRow.midrow;

            missile_fx.target_y = missile.targetPlayer ? FlightFX.YRow.player : FlightFX.YRow.enemy;
            //slgithy longer flight time for miss to not have rockets zoom.
            missile_fx.target_flight_time = will_hit ? 0.4 : 0.6;
            this.timer = missile_fx.target_flight_time + 0.2;
            //add to fx of combat.
            c.fx.Add(missile_fx);

            //reduce cluster size
            missile.stackSize--;
            if (missile.stackSize == 0)
            {
                c.stuff.Remove(world_pos_x);
            }
        }

        public override void Update(G g, State s, Combat c)
        {
            timer -= g.dt;
            if (timer < 0.2 && !done)
            {
                if (will_hit)
                    OnHit(g, s, c);
                done = true;
            }
        }

        /// <summary>
        /// Mostly copied code from missile hit.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="c"></param>
        private void OnHit(G g, State s, Combat c)
        {
            if (missile == null)
            {
                if (!c.stuff.TryGetValue(world_pos_x, out var stuff) || stuff is not ClusterMissile msl)
                {
                    return;
                }
                missile = msl;
            }

            int incomingDamage = missile.RawDamage();
            foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                incomingDamage += enumerateAllArtifact.ModifyBaseMissileDamage(s, s.route as Combat, this.missile.targetPlayer);

            if (incomingDamage < 0)
                incomingDamage = 0;
            var target = missile.targetPlayer ? s.ship : c.otherShip;
            DamageDone dmg = target.NormalDamage(s, c, incomingDamage, target_pos);

            EffectSpawner.NonCannonHit(g, missile.targetPlayer, new RaycastResult() { fromDrone = true, hitDrone = false, hitShip = true, worldX = target_pos }, dmg);

            Part? partAtWorldX = target.GetPartAtWorldX(target_pos);

            if ((partAtWorldX != null ? (partAtWorldX.stunnable ? 1 : 0) : 0) != 0)
                c.QueueImmediate((CardAction)new AStunPart()
                {
                    worldX = target_pos
                });

            if (target.Get(Status.payback) > 0 || target.Get(Status.tempPayback) > 0)
            {
                c.QueueImmediate((CardAction)new AAttack()
                {
                    damage = Card.GetActualDamage(s, target.Get(Status.payback) + target.Get(Status.tempPayback), !missile.targetPlayer),
                    targetPlayer = !missile.targetPlayer,
                    fast = true,
                });
            }
        }
    }
}