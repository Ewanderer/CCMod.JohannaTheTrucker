﻿using FMOD;
using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class SaturationFire : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var list = new List<CardAction>();

            switch (upgrade)
            {
                case Upgrade.None:

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset=-1
                        };

                        list.Add(spawn_cluster);
                    }

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = 1
                        };

                        list.Add(spawn_cluster);
                    }

                    break;
                case Upgrade.A:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 4
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = -1
                        };

                        list.Add(spawn_cluster);
                    }

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 4
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = 1
                        };

                        list.Add(spawn_cluster);
                    }
                    break;
                case Upgrade.B:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = -1
                        };

                        list.Add(spawn_cluster);
                    }

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = 0
                        };

                        list.Add(spawn_cluster);
                    }

                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile,
                            offset = 1
                        };

                        list.Add(spawn_cluster);
                    }
                    break;
            }

            return list;
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = 1
            };
        }

        public override string Name() => "Saturation Fire";
    }
}