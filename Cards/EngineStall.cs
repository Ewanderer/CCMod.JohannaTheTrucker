using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class EngineStall : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            if (upgrade != Upgrade.A)
                result.Add(new AStatus()
                {
                    mode = AStatusMode.Set,
                    statusAmount = 0,
                    status = Status.evade,
                    targetPlayer=true
                });

            result.Add(new AStatus()
            {
                statusAmount = 1,
                status = Status.loseEvadeNextTurn,
                targetPlayer=true,
            });

            result.Add(new AEnergy()
            {
                changeAmount = 1,
            });

            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 0,
                retain = upgrade == Upgrade.B
            };
        }

        public override string Name() => "Engine Stall";
    }
}
