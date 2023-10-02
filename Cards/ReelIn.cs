using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohannaTheTrucker.Actions;

namespace JohannaTheTrucker.Cards
{
    public class ReelIn : Card
    {

        bool flipped = false;

        public override List<CardAction> GetActions(State s, Combat c)
        {
           var list=new List<CardAction>();

            list.Add(new AHook()
            {
                hookToRight = flipped
            });

            list.Add(new ADroneMove()
            {
                dir = flipped ? 1 : -1,            
            });
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

        public override string Name() => "Reel In"; 

        public override void OnFlip(G g)
        {
            flipped = !flipped;
        }
    }
}
