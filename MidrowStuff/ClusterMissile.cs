using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.MidrowStuff
{
    public class ClusterMissile : StuffBase
    {
        public int stackSize;

        public override List<CardAction>? GetActions(State s, Combat c)
        {
            return new List<CardAction>() {
            new AAttack(){
            damage=GetDamage(),
            targetPlayer=this.targetPlayer,

            }
            };
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
            //draw swarm
            var pattern = GetPattern(stackSize);
            var offset = this.GetOffset(g);
            for (int i = 0; i < Math.Min(stackSize, 5); i++)
            {

                //calc position.
                var x = v.x + pattern[i].x + offset.x-4;
                var y = v.y + pattern[i].y + offset.y;
                if (highligthed)
                {
                    x -= 2;
                    y -= 2;
                    Draw.Sprite(outlined, x, y, flip, !flip, 0, null, null, null, null, null, BlendMode.Screen, null, null);
                }
                else
                {
                    Draw.Sprite(spr, x, y, flip, !flip, 0, null, null, null, null, null, null, null, null);
                }
            }


        }

        private Vec[] GetPattern(int length)
        {
            if (length < 0)
                length = 0;
            if (length > 5)
                length = 5;
            var pattern = new Vec[length];

            for (int i = 0; i < length; i++)
                pattern[i] = new Vec(8, 16);
            switch (length)
            {
                case 2:
                    pattern[0] += new Vec(0, -8);
                    pattern[1] += new Vec(0, 8);
                    break;
                case 3:
                    pattern[0] += new Vec(-4, 8);
                    pattern[1] += new Vec(4, 8);
                    pattern[2] += new Vec(0, -8);
                    break;
                case 4:
                    pattern[0] += new Vec(-4, 8);
                    pattern[1] += new Vec(4, 8);
                    pattern[2] += new Vec(-4, -8);
                    pattern[3] += new Vec(4, -8);
                    break;

                case 5:
                    pattern[0] += new Vec(-4, 12);
                    pattern[1] += new Vec(4, 12);
                    pattern[2] += new Vec(-4, -12);
                    pattern[3] += new Vec(4, -12);
                    pattern[4] += new Vec(0, 0);
                    break;
            }
            return pattern;
        }


        public override Spr? GetIcon() => (Spr)(Manifest.ClusterMissleIcon?.Id ?? throw new Exception("missing hook icon"));

        public virtual void GrowCluster(ClusterMissile otherCluster)
        {
            //check if upgrade to HE missile is necessary.
            stackSize += otherCluster.GetActualClusterSize();
        }
    }
}
