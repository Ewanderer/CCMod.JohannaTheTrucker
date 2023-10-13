using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class LargePayload : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
                case Upgrade.None:
                case Upgrade.A:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 6
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    break;
                case Upgrade.B:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 6,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile
                        };


                        result.Add(spawn_cluster);

                        result.Add(new AStatus()
                        {
                            status = Status.droneShift,
                            statusAmount = 2,
                            targetPlayer = true
                        });


                    }
                    break;
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
                exhaust = upgrade != Upgrade.A
            };
        }

        public override string Name() => "Large Payload";

    }
}
