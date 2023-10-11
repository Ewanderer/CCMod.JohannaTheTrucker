using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common)]
    public class ClusterRocket : Card
    {


        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            {
                var cluster_missile = new MidrowStuff.ClusterMissile()
                {
                    stackSize = 3
                };

                var spawn_cluster = new ASpawnCluster()
                {
                    cluster = cluster_missile
                };

                result.Add(spawn_cluster);
            }
            {
                var cluster_missile = new MidrowStuff.ClusterMissile()
                {
                    stackSize = 3,
                    stackType = MidrowStuff.ClusterMissile.MissileType.heavy
                };

                var spawn_cluster = new ASpawnCluster()
                {
                    cluster = cluster_missile
                };

                result.Add(spawn_cluster);
            }

            {
                var cluster_missile = new MidrowStuff.ClusterMissile()
                {
                    stackSize = 3,
                    stackType = MidrowStuff.ClusterMissile.MissileType.seeker
                };

                var spawn_cluster = new ASpawnCluster()
                {
                    cluster = cluster_missile
                };

                result.Add(spawn_cluster);
            }

            {
                var cluster_missile = new MidrowStuff.ClusterMissile()
                {
                    stackSize = 1,
                    bubbleShield = true
                };

                var spawn_cluster = new ASpawnCluster()
                {
                    cluster = cluster_missile,
                };

                result.Add(spawn_cluster);
            }

            return result;

        }

        public override string Name() => "Cluster Rocket";

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
            };
        }


    }
}
