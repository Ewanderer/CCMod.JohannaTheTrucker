using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class VarietyPack : Card
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
                            stackSize = 1,
                            stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 1
                    });

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 1,
                            stackType = MidrowStuff.ClusterMissile.MissileType.normal
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }

                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 1
                    });

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 1,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
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
                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 1
                    });

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2,
                            stackType = MidrowStuff.ClusterMissile.MissileType.normal
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }

                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 1
                    });

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
                    }
                    break;
                case Upgrade.B:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2,
                            stackType = MidrowStuff.ClusterMissile.MissileType.normal
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 1
                    });

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 1,
                            stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }

                    result.Add(new AMove()
                    {
                        targetPlayer = true,
                        dir = 3
                    });

                    {
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
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 3
            };
        }

        public override string Name() => "Variety Pack";
    }
}
