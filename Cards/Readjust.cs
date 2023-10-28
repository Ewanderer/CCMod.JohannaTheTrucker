using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class Readjust : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var pop_bubble_status = (Status)(Manifest.PopBubblesStatus?.Id ?? throw new Exception("Missing PopBubblesStatus"));
            var lose_drone_shift_status = (Status)(Manifest.LoseDroneShiftStatus?.Id ?? throw new Exception("Missing LoseDroneShiftStatus"));
            var result = new List<CardAction>() {
            new AStatus(){
            status=Status.evade,
            statusAmount=upgrade==Upgrade.B?3:2,
            targetPlayer=true
            },
            new AStatus(){
            status=Status.droneShift,
            statusAmount=2,
            targetPlayer=true
            },
            new ABubbleField(),

            };

            if (upgrade != Upgrade.A)
            {
                result.Add(new AStatus()
                {
                    status = Status.loseEvadeNextTurn,
                    statusAmount = 1,
                    targetPlayer = true
                });

                if (s.ship.Get(lose_drone_shift_status) == 0)
                    result.Add(new AStatus()
                    {
                        status = lose_drone_shift_status,
                        statusAmount = 1,
                        targetPlayer = true
                    });
            }

            if (upgrade != Upgrade.B)
            {
                result.Add(new AStatus()
                {
                    status = pop_bubble_status,
                    statusAmount = 1,
                    targetPlayer = true
                });
            }

            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
                exhaust = true
            };
        }

        public override string Name() => "Readjust";
    }
}
