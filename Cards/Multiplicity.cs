using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class Multiplicity : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            if (upgrade == Upgrade.A)
                result.Add(new AStatus()
                {
                    targetPlayer = true,
                    status = Status.droneShift,
                    statusAmount = 2
                });

            result.Add(new AGrowClusters()
            {
                ammount = upgrade == Upgrade.B ? 4 : 2,
                cluster_type = MidrowStuff.ClusterMissile.MissileType.normal,
                fromPlayer = true
            });

            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
                exhaust = upgrade == Upgrade.B
            };
        }

        public override string Name() => "Multiplicity";
    }
}
