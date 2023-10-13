using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class EnemyShift : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            result.Add(new AMove()
            {
                dir = upgrade == Upgrade.B ? 4 : 1,
                fromEvade = false,
                targetPlayer = false
            });

            result.Add(new AStatus()
            {
                status = Status.droneShift,
                statusAmount = 1,
                targetPlayer = true
            });
            result.Add(new AStatus()
            {
                status = Status.evade,
                statusAmount = upgrade == Upgrade.A ? 2 : 1,
                targetPlayer = true
            });
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 1,
                exhaust = upgrade == Upgrade.B
            };
        }

        public override string Name() => "Enemy Shift";
    }
}
