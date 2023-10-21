using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class MassUpgrade : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            if (upgrade == Upgrade.B)
            {

                result.Add(new AGrowClusters()
                {
                    ammount = 1,
                    cluster_type = MidrowStuff.ClusterMissile.MissileType.seeker,
                    fromPlayer = true
                });

                result.Add(new AGrowClusters()
                {
                    ammount = 0,
                    cluster_type = MidrowStuff.ClusterMissile.MissileType.heavy,
                    fromPlayer = true
                });

            }
            else
            {

                result.Add(new AGrowClusters()
                {
                    ammount = 2,
                    cluster_type = MidrowStuff.ClusterMissile.MissileType.heavy,
                    fromPlayer = true,
                    disabled = flipped
                });

                result.Add(new ADummyAction() { });

                result.Add(new AGrowClusters()
                {
                    ammount = 2,
                    cluster_type = MidrowStuff.ClusterMissile.MissileType.seeker,
                    fromPlayer = true,
                    disabled = !flipped
                });
            }

            return result;
        }

        public override CardData GetData(State state)
        {
            int cost;
            switch (upgrade)
            {
                case Upgrade.None:
                    cost = 2;
                    break;
                case Upgrade.A:
                    cost = 1;
                    break;
                case Upgrade.B:
                    cost = 0;
                    break;
                default:
                    throw new NotImplementedException($"Upgrade {upgrade} unkown!");
            }

            return new CardData
            {
                cost = cost,
                exhaust = true,
                floppable = upgrade != Upgrade.B,
            };
        }

        public override string Name() => "Mass Upgrade";

    }
}
