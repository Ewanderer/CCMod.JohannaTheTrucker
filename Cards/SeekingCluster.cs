using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class SeekingCluster : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
                case Upgrade.None:
                    {
                        result.Add(new ADroneMove()
                        {
                            dir = -1
                        });

                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 3,
                            stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    break;
                case Upgrade.A:
                    {

                        result.Add(new ADroneMove()
                        {
                            dir = -3
                        });

                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 5,
                            stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    break;
                case Upgrade.B:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2,
                            stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset = -2
                        };

                        result.Add(spawn_cluster);
                    }

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2,
                            stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }

                    break;
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1
            };
        }

        public override string Name() => "Seeking Cluster";
    }
}
