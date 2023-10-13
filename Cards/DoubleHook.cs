using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class DoubleHook : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            return new List<CardAction>
            {
                new AHook()
                {
                    hookToRight = flipped
                },

                new AHook()
                {
                    hookToRight = flipped
                },

                new AStatus()
                {
                    status = Status.evade,
                    statusAmount = 1
                }
            };
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 1,
                flippable=upgrade==Upgrade.A,
                recycle=upgrade==Upgrade.B,
            };
        }

        public override string Name() => "Double Hook";
    }
}
