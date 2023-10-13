using JohannaTheTrucker.Actions;
using Microsoft.Xna.Framework.Graphics;
using static System.Linq.Enumerable;

namespace JohannaTheTrucker.MidrowStuff
{
    public class ClusterMissile : StuffBase
    {
        public int stackSize;
        public MissileType stackType = MissileType.normal;
        private const int resolution = 1000;

        /// <summary>
        /// Cache sine values
        /// </summary>
        private static double[] sin_cache = Range(0, resolution).Select(x => Math.Sin(((double)x / (double)resolution) / Math.PI * 20)).ToArray();

        private double speed = 3;
        private int time_step_counter = -1;

        public enum MissileType
        {
            normal = 0,
            heavy = 1,
            seeker = 2,
            heavy_seeker = 3
        }

        public Spr? GetActionIcon()
        {
            switch (stackType)
            {
                case MissileType.normal:
                    return (Spr)(Manifest.ClusterMissleIcon?.Id ?? throw new Exception("missing missile icon"));

                case MissileType.heavy:
                    return (Spr)(Manifest.HEClusterMissleIcon?.Id ?? throw new Exception("missing missile icon"));

                case MissileType.seeker:
                    return (Spr)(Manifest.SeekerClusterMissleIcon?.Id ?? throw new Exception("missing missile icon"));

                default:
                    return null;
            }
        }

        public override List<CardAction>? GetActions(State s, Combat c)
        {
            return new List<CardAction>() {
                new AClusterAttack(){
                world_pos_x=this.x,
                }
            };
        }

        public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
        {
            return new List<CardAction>();
        }

        public override Spr? GetIcon()
        {
            switch (stackType)
            {
                case MissileType.normal:
                    return (Spr)(Manifest.ClusterMissleToken?.Id ?? throw new Exception("missing missile icon"));

                case MissileType.heavy:
                    return (Spr)(Manifest.HEClusterMissleToken?.Id ?? throw new Exception("missing missile icon"));

                case MissileType.seeker:
                    return (Spr)(Manifest.SeekerClusterMissleToken?.Id ?? throw new Exception("missing missile icon"));

                case MissileType.heavy_seeker:
                    return (Spr)(Manifest.HESeekerClusterMissleToken?.Id ?? throw new Exception("missing missile icon"));

                default:
                    return null;
            }
        }

        public virtual void GrowCluster(ClusterMissile otherCluster, Combat c, State s)
        {
            if (otherCluster.targetPlayer != targetPlayer)
            {
                ResolveOpposingClusterCollision(otherCluster, c,s);
               
                return;
            }
            //check if upgrade to HE missile is necessary.
            stackSize += otherCluster.stackSize;
            //check if there is an possible upgrade
            if (otherCluster.stackType != MissileType.normal && stackType != otherCluster.stackType)
            {
                //check if the upgrade is just to the other type
                if (stackType == MissileType.normal)
                    stackType = otherCluster.stackType;
                //of if it becomes the ultimate version.
                else stackType = MissileType.heavy_seeker;
            }

            if (otherCluster.bubbleShield)
                bubbleShield = true;
        }

        public int RawDamage()
        {
            if (stackType == MissileType.heavy || stackType == MissileType.heavy_seeker)
                return 2;
            return 1;
        }

        public override void Render(G g, Vec v)
        {
            if (age < 0.2)
                return;
            if (time_step_counter != -1)
            {
                var step_size = (int)Math.Round(Math.Clamp(g.dt / speed * 2 * (double)resolution, 1.0, (double)resolution / 4));
                time_step_counter = (time_step_counter + step_size) % (2 * resolution);
            }
            else
            {
                //first setup to sync.
                var init_val = (int)Math.Round((g.time / speed) % (2.0 * (double)resolution));
                time_step_counter = init_val;
            }
            bool highligthed = ShouldDrawHilight(g);
            Texture2D? outlined = null;
            var spr = GetIcon();
            if (spr == null)
                return;
            if (highligthed)
            {
                outlined = SpriteLoader.GetOutlined(spr.Value);
            }
            var flip = !targetPlayer;
            //rotation raycastr
            //  var target_v = targetPlayer ? new Vec(0, -1) : new Vec(0, 1);
            //  var angle = Math.Atan2(target_v.y - 1, target_v.x);

            var offset_per_missile = (int)Math.Round(Math.Clamp(((double)resolution * 2) / stackSize, 1, resolution * 2));
            for (int i = 0; i < stackSize; i++)
            {
                var delta = (time_step_counter + offset_per_missile * i) % (resolution * 2);
                var offset = OffestFromStep(delta);
                var draw_y = offset.x * 20;
                var draw_x = offset.y * 4 + 4;

                if (highligthed)
                {
                    draw_x -= 2;
                    draw_y -= 2;
                    Draw.Sprite(outlined, v.x + draw_x, v.y + draw_y, flip, !flip, 0, null, null, null, null, null, BlendMode.Screen, null, null);
                }
                else
                {
                    Draw.Sprite(spr, v.x + draw_x, v.y + draw_y, flip, !flip, 0, null, null, null, null, null, null, null, null);
                }
            }
        }

        private int GetDamage()
        {
            return 1;
        }

        private Vec OffestFromStep(int i)
        {
            double y;
            double x;
            if (i < resolution)
            {
                x = (double)i / (double)resolution;
            }
            else
            {
                i -= resolution;
                x = 1 - (double)i / (double)resolution;
            }
            y = sin_cache[i];
            return new Vec(x, y);
        }

        /// <summary>
        /// Opposing clusters should annihilate each other
        ///  If bubbled, the new instance will be destroyed on launch(normal behavior), pop the existing bubble(also normal behavior) and and cluster charges and properties from the destroyed instance isn't added or merged at all
        /// </summary>
        /// <param name="otherCluster"></param>
        private void ResolveOpposingClusterCollision(ClusterMissile otherCluster, Combat c, State s)
        {

            //trigger artifacts
            if (bubbleShield == otherCluster.bubbleShield)
            {
                c.fx.Add(new DroneExplosion()
                {
                    pos = FxPositions.Drone(this.x)
                });          

                //destroy this instance
                c.stuff.Remove(this.x);
                if (otherCluster.fromPlayer)
                    foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                        enumerateAllArtifact.OnPlayerDestroyDrone(s, c);
                Audio.Play(FSPRO.Event.Hits_DroneCollision);

            }
            else if (bubbleShield && !bubbleShield)
            {
                //pop bubble
                bubbleShield = false;
                c.fx.Add(new ShieldPop()
                {
                    pos = FxPositions.Drone(this.x)
                });
            }
            else
            {
                //triger destroyed drone artifact

                c.fx.Add(new DroneExplosion()
                {
                    pos = FxPositions.Drone(this.x)
                });

                //replace target, ammout and type.
                this.targetPlayer = otherCluster.targetPlayer;
                this.stackType = otherCluster.stackType;
                this.stackSize = otherCluster.stackSize;
                if (otherCluster.fromPlayer)
                    foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                        enumerateAllArtifact.OnPlayerDestroyDrone(s, c);
                Audio.Play(FSPRO.Event.Hits_DroneCollision);
            }
        }
    }
}