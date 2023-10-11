using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Enumerable;

namespace JohannaTheTrucker.MidrowStuff
{
    public class ClusterMissile : StuffBase
    {
        private const int resolution = 1000;
        /// <summary>
        /// Cache sine values
        /// </summary>
        private static double[] sin_cache = Range(0, resolution).Select(x => Math.Sin(((double)x / (double)resolution) / Math.PI * 20)).ToArray();

        private int time_step_counter = 0;

        public int stackSize;

        public enum MissileType
        {
            normal = 0,
            heavy = 1,
            seeker = 2,
            heavy_seeker = 3
        }

        public MissileType stackType = MissileType.normal;

        private double speed = 3;

        public override List<CardAction>? GetActions(State s, Combat c)
        {
            return new List<CardAction>() {
            new AAttack(){
            damage=GetDamage(),
            targetPlayer=this.targetPlayer,

            }
            };
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

        private int GetDamage()
        {
            return 1;
        }

        public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
        {
            return new List<CardAction>();
        }

        public virtual int GetActualClusterSize()
        {
            return stackSize;
        }



        public override void Render(G g, Vec v)
        {
            var step_size = (int)Math.Round(Math.Clamp(g.dt / speed * 2 * (double)resolution, 1.0, (double)resolution / 4));

            time_step_counter = (time_step_counter + step_size) % (2 * resolution);

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
            var size = GetActualClusterSize();
            var offset_per_missile = (int)Math.Round(Math.Clamp(((double)resolution * 2) / size, 1, resolution * 2));
            for (int i = 0; i < size; i++)
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

        public virtual void GrowCluster(ClusterMissile otherCluster)
        {
            //check if upgrade to HE missile is necessary.
            stackSize += otherCluster.GetActualClusterSize();
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
    }
}
