using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class SmartExplosives : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var smart_explosives_status = (Status)(Manifest.SmartExplosiveStatus?.Id ?? throw new Exception("Missing RocketSiloStatus"));
            return new List<CardAction>()
            {
                new AStatus(){
                targetPlayer=true,
                status=smart_explosives_status,
                statusAmount=upgrade==Upgrade.B?99:2,
                }
            };


        }

        public override CardData GetData(State state)
        {
            int cost = 0;
            switch (upgrade)
            {
                case Upgrade.None:
                    cost = 2;
                    break;
                case Upgrade.A:
                    cost = 1;
                    break;
                case Upgrade.B:
                    cost = 3;
                    break;
            }

            return new CardData
            {
                cost = cost,
                exhaust = upgrade == Upgrade.B
            };
        }

        public override string Name() => "Smart Explosives";
    }
}
