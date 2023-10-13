using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class SmallManeuver : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
               
                case Upgrade.None:
                    result.Add(new AStatus() { 
                    status=Status.evade,
                    statusAmount=1
                    });
                    result.Add(new AStatus()
                    {
                        status=Status.tempShield, 
                        statusAmount=1
                    });
                    break;
                case Upgrade.A:
                    result.Add(new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 1
                    });
                    result.Add(new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1
                    });
                    break;
                case Upgrade.B:
                    result.Add(new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 1
                    });
                    result.Add(new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 2
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
                retain = upgrade == Upgrade.B
            };
        }

        public override string Name() => "Small Maneuver";
    }
}
