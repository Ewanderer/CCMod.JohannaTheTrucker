using FMOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker.Actions
{
    /// <summary>
    /// An extension of the normal ASpan action to include the logic for cluster missiles.
    /// </summary>
    public class ASpawnCluster : ASpawn
    {

        public  ClusterMissile? cluster_internal;
        
        public ClusterMissile cluster
        {
            get => cluster_internal ?? throw new Exception("cluster missile must be assigned!");
            set
            {
                cluster_internal = value;
                thing = value;
            }
        }


        public override void Begin(G g, State s, Combat c)
        {
            if (cluster == null)
                return;
            Ship ship = this.fromPlayer ? s.ship : c.otherShip;

            if (!this.fromX.HasValue && ship.parts.FindIndex(e => e.type == PType.missiles && e.active) == -1)
                return;
            if (this.fromPlayer && g.state.ship.GetPartTypeCount(PType.missiles) > 1 && !this.multiBayVolley)
            {
                c.QueueImmediate((CardAction)new AVolleySpawnFromAllMissileBays()
                {
                    spawn = Mutil.DeepCopy<ASpawnCluster>(this)
                });
                this.timer = 0.0;
            }
            else
            {

                this.cluster.fromPlayer = this.fromPlayer;
                if (this.fromPlayer)
                {
                    foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                        enumerateAllArtifact.OnPlayerSpawnSomething(s, c);
                }
                StuffBase actual_launch = cluster;
                foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                    actual_launch = enumerateAllArtifact.ReplaceSpawnedThing(s, c, actual_launch, this.fromPlayer);


                int worldX1 = this.GetWorldX(s, c);
                int worldX2 = worldX1;
                int worldX3 = worldX1 + this.offset;
                Part? partAtWorldX = ship.GetPartAtWorldX(worldX2);
                if (partAtWorldX != null)
                    partAtWorldX.pulse = 1.0;
                ParticleBursts.DroneSpawn(worldX3, this.fromPlayer);
                if (ship.Get(Status.backwardsMissiles) > 0)
                    actual_launch.targetPlayer = !actual_launch.targetPlayer;
                if (!actual_launch.bubbleShield && ship.Get(Status.bubbleJuice) > 0)
                {
                    actual_launch.bubbleShield = true;
                    ship.Set(Status.bubbleJuice, ship.Get(Status.bubbleJuice) - 1);
                }

                if (c.stuff.TryGetValue(worldX3, out StuffBase? stuffBase))
                {
                    //Check if we are still laucning a cluster missile and if our target is a cluster missile
                    if (actual_launch is ClusterMissile launched_cluster && stuffBase is ClusterMissile otherCluster)
                    {
                        HandleClusterGrowth(otherCluster, s, c, launched_cluster, worldX3);
                    }
                    else
                    {
                        //if not we use the boring old code here.
                        HandleNormalCollision(stuffBase, s, c, actual_launch, worldX3);
                    }
                }
                else
                {
                    Audio.Play(new GUID?(FSPRO.Event.Drones_MissileLaunch));
                    actual_launch.xLerped = (double)(actual_launch.x = worldX3);
                    actual_launch.yAnimation = 0.0;
                    c.stuff.Add(worldX3, actual_launch);
                    if (actual_launch.droneNameAccordingToIsaac == null)
                        return;
                    s.storyVars.lastNamedDroneSpawned = this.cluster.droneNameAccordingToIsaac;
                }
            }
        }


        private void HandleClusterGrowth(ClusterMissile otherCluster, State s, Combat c, ClusterMissile launched_cluster, int worldX3)
        {
            //make feedback
            Audio.Play(new GUID?(FSPRO.Event.Drones_MissileLaunch));
#warning to-do: FX for a launch graphic
            //grow other cluster
            otherCluster.GrowCluster(launched_cluster);
        }

        private void HandleNormalCollision(StuffBase stuffBase, State s, Combat c, StuffBase actual_launch, int worldX3)
        {
            bool flag = stuffBase.Invincible();
            bool bubbleShield = stuffBase.bubbleShield;
            Audio.Play(new GUID?(FSPRO.Event.Hits_DroneCollision));
            stuffBase.DoDestroyedEffect(s, c);
            if (this.fromPlayer)
            {
                if (!flag && !bubbleShield)
                {
                    foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                        enumerateAllArtifact.OnPlayerDestroyDrone(s, c);
                }
                foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                    enumerateAllArtifact.OnPlayerDestroyDrone(s, c);
            }
            if (!flag && !bubbleShield)
                c.QueueImmediate(stuffBase.GetActionsOnDestroyed(s, c, this.fromPlayer, worldX3));
            c.QueueImmediate(actual_launch.GetActionsOnDestroyed(s, c, this.fromPlayer, worldX3));
            if (bubbleShield)
            {
                stuffBase.bubbleShield = false;
            }
            else
            {
                if (flag)
                    return;
                c.stuff.Remove(worldX3);
            }
        }

        /*
        public override List<Tooltip> GetTooltips(State s)
        {
            List<Tooltip> tooltips = new List<Tooltip>();
            int x = s.ship.x;
            foreach (Part part in s.ship.parts)
            {
                if (part.type == PType.missiles && part.active)
                {
                    if (s.route is Combat route && route.stuff.ContainsKey(x))
                        route.stuff[x].hilight = 2;
                    part.hilight = true;
                }
                ++x;
            }
            if (this.offset == 0)
                tooltips.Add((Tooltip)new TTGlossary("action.spawn", Array.Empty<object>()));
            else
                tooltips.Add((Tooltip)new TTGlossary(this.offset < 0 ? "action.spawnOffsetLeft" : "action.spawnOffsetRight", new object[1]
                {
         Math.Abs(this.offset)
                }));
            if (cluster != null)
                tooltips.AddRange((IEnumerable<Tooltip>)this.cluster.GetTooltips());
            return tooltips;
        }
        */


        public override Icon? GetIcon(State s)
        {
            if (Manifest.ClusterMissleIcon?.Id == null)
                return null;
            bool flipY = this.cluster?.targetPlayer ?? false;
            if (this.fromPlayer && s.ship.Get(Status.backwardsMissiles) > 0)
                flipY = !flipY;
          return new Icon((Spr)Manifest.ClusterMissleIcon.Id, cluster?.GetActualClusterSize() ?? 0, Colors.textMain, flipY);
     
        }

    }
}
