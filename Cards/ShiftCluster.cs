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
                case Upgrade.A:
                    {
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 3
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset=1
                        };

                        list.Add(spawn_cluster);
                    }

                                        break;
                case Upgrade.B:
                    {
						var hook_action = new AHook()
						{
							//hookToRight = flipped
						};

						hook_action.disabled = hook_action.CalculateMove(s, c) == null;

						list.Add(hook_action);
						
                        var cluster_missile = new MidrowStuff.ClusterMissile()
                        {
                            stackSize = 4
                        };

                        var spawn_cluster = new ASpawn()
                        {
                            thing = cluster_missile,
                            offset = 0
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
				flippable = upgrade == Upgrade.A,
            };
        }

        public override string Name() => "Shift Cluster";
    }
}
