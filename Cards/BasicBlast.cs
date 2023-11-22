using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(deck =Deck.colorless, rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B },dontOffer =true)]
    public class BasicBlast : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {

                case Upgrade.None:
                    result.Add(new AAttack() { 
                    damage=1,
                    targetPlayer=false,
                    });
                    break;
                case Upgrade.A:
                    result.Add(new AAttack()
                    {
                        damage = 1,
                        targetPlayer = false,
                    });
                    result.Add(new AAttack()
                    {
                        damage = 1,
                        targetPlayer = false,
                    });
                    break;
                case Upgrade.B:
                    result.Add(new AAttack()
                    {
                        damage = 1,
                        targetPlayer = false,
                        piercing=true,
                    });
                    break;

            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 1,
            };
        }

        public override string Name() => "Basic Blast";

    }
}
