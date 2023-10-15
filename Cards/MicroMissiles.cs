using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, dontOffer = true)]
    public class MicroMissiles : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            var cluster_missile = new MidrowStuff.ClusterMissile()
            {
                stackSize = 2
            };

            var spawn_cluster = new ASpawn()
            {
                thing = cluster_missile
            };

            result.Add(spawn_cluster);
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 0,
                temporary = true,
                exhaust=true,
            };
        }

        public override string Name() => "Micro Missiles";

    }
}
