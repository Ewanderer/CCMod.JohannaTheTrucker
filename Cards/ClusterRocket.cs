using JohannaTheTrucker.Actions;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class ClusterRocket : Card
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
                            stackSize = 3
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile
                        };

                        result.Add(spawn_cluster);
                    }
                    break;
                case Upgrade.A:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 5
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
                            stackSize = 2
                        };

                        var spawn_cluster = new ASpawnCluster()
                        {
                            cluster = cluster_missile
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
                cost = upgrade == Upgrade.B ? 0 : 1,
            };
        }

        public override string Name() => "Cluster Rocket";
    }
}