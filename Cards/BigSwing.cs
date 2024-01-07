using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class BigSwing : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>
            {
                new AStatus()
                {
                    status = Status.hermes,
                    statusAmount = upgrade == Upgrade.A ? 3 : 2,
                    targetPlayer=true
                }
            };
            var hook_action = new AHook()
            {
                hookToRight = flipped,
                fromPlayer = true,

            };
            hook_action.disabled = hook_action.CalculateMove(s, c, out _) == null;

            result.Add(hook_action);

            if (upgrade == Upgrade.B)
            {
                result.Add(new AMove()
                {
                    targetPlayer = true,
                    dir = -2,
                });
            }
            return result;




        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 2,
                retain = upgrade == Upgrade.B,
                exhaust = upgrade == Upgrade.B,
                flippable = true,
            };
        }

        public override string Name() => "Big Swing";
    }
}