using JohannaTheTrucker.Actions;
using JohannaTheTrucker.MidrowStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class Rebound : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {


            var cluster = new ClusterMissile()
            {
                stackSize = upgrade == Upgrade.A ? 3 : 2
            };

            var hook_action = new AHook()
            {
                hookToRight = flipped
            };

            hook_action.disabled = hook_action.CalculateMove(s, c) == null;

            return new List<CardAction>()
            {
                hook_action,
                new AMove(){
                    targetPlayer=true,
                    dir=upgrade==Upgrade.B?5: 2,
                },
                new ASpawn()
                {
                fromPlayer=true,
                thing=cluster,
                }
            };


        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
                flippable = true,
                retain = upgrade == Upgrade.B,
            };
        }

        public override string Name() => "Rebound";
    }
}
