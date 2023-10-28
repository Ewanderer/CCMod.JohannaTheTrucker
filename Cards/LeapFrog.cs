using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class LeapFrog : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
                case Upgrade.A:
                case Upgrade.None:
                    result.Add(new ADroneMove()
                    {
                        dir = -1
                    });
                    result.Add(new AHook()
                    {
                        hookToRight = flipped,
                    });
                    result.Add(new AMove()
                    {
                        dir = -1,
                        fromEvade = false,
                        targetPlayer=true,
                    });
                    break;
                case Upgrade.B:
                    result.Add(new ADroneMove()
                    {
                        dir = -2
                    });
                    result.Add(new AHook()
                    {
                        hookToRight = flipped
                    });
                    result.Add(new AMove()
                    {
                        dir = -2,
                        fromEvade = false,
			targetPlayer=true,
                    });
                    break;
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
                flippable = true,
                recycle = upgrade == Upgrade.A,
				exhaust = upgrade == Upgrade.B,
            };
        }

        public override string Name() => "Leap Frog";
    }
}
