using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class HECluster : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
                case Upgrade.None:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 3,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                        result.Add(new AStatus()
                        {
                            status = Status.droneShift,
                            statusAmount = 1,
                            targetPlayer = true
                        });
                    }
                    break;
                case Upgrade.A:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };


                        result.Add(spawn_cluster);
                        result.Add(new AStatus()
                        {
                            status = Status.droneShift,
                            statusAmount = 1,
                            targetPlayer = true
                        });
                    }
                    break;
                case Upgrade.B:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 3,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset = -1
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
                cost = upgrade == Upgrade.A ? 1 : 2,
                flippable = upgrade == Upgrade.B
            };
        }

        public override string Name() => "HE-Cluster";

    }
}
