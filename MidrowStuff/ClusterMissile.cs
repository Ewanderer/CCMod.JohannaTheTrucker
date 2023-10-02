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

        private int GetDamage() {
            return 1;
        }

        public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
        {
            return new List<CardAction>();
        }

        public virtual int GetActualClusterSize() { 
             return stackSize;
        }

        public override Spr? GetIcon() => (Spr)(Manifest.HookIcon?.Id ?? throw new Exception("missing hook icon"));        

        public virtual void GrowCluster(ClusterMissile otherCluster) {
//check if upgrade to HE missile is necessary.
            stackSize += otherCluster.GetActualClusterSize();
        }
    }
}
