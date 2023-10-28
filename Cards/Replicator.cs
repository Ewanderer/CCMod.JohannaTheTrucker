using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class Replicator : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            if (upgrade != Upgrade.B)
            {



                var can_play = c.hand.Count > 0;
                var sel = (int)Math.Round((c.hand.Count - 1) * s.rngActions.Next());

                result.Add(new ADiscard()
                {
                    count = 1,
                    ignoreRetain = true,
                    selectedCard = c.hand.Count > 0 ? c.hand[sel] : null,
                    disabled=!can_play,
                });

                {
                    var cluster_missile = new MidrowStuff.ClusterMissile()
                    {
                        stackSize = upgrade == Upgrade.A ? 1 : 2
                    };

                    var spawn_cluster = new ASpawn()
                    {
                        thing = cluster_missile,
                        disabled = !can_play
                    };

                    result.Add(spawn_cluster);
                }

                if (upgrade == Upgrade.A)
                {
                    var cluster_missile = new MidrowStuff.ClusterMissile()
                    {
                        stackSize = 1,
                    };

                    var spawn_cluster = new ASpawn()
                    {
                        thing = cluster_missile,
                        disabled = !can_play,
                        offset = 1
                    };

                    result.Add(spawn_cluster);
                }

            }
            else
            {
                {
                    var cluster_missile = new MidrowStuff.ClusterMissile()
                    {
                        stackSize = 99
                    };

                    var spawn_cluster = new ASpawn()
                    {
                        thing = cluster_missile,
                    };

                    result.Add(spawn_cluster);

                    result.Add(new AEndTurn() { });
                }
            }

            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = upgrade == Upgrade.B ? 2 : 0,
                infinite = upgrade != Upgrade.B,
                exhaust = upgrade == Upgrade.B,
            };
        }

        public override string Name() => "Replicator";
    }
}
