using FMOD;
using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class ShiftCluster : Card
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
                            stackSize = 3
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset=3
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

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset=4
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

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset=6
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
				cost = 1,
				flippable = true,
            };
        }

        public override string Name() => "Shift Cluster";
    }
}
