using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class RocketSilo : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var rocket_silo_status = (Status)(Manifest.RocketSiloStatus?.Id ?? throw new Exception("Missing RocketSiloStatus"));
            return new List<CardAction>()
            {
                new AStatus(){
                targetPlayer=true,
                status=rocket_silo_status,
                statusAmount=1,
                }
            };


        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = upgrade == Upgrade.A ? 2 : 3,
                exhaust = upgrade != Upgrade.B
            };
        }

        public override string Name() => "Rocket Silo";
    }
}
