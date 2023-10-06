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

        public override List<CardAction> GetActions(State s, Combat c)
        {
           var list=new List<CardAction>();

            list.Add(new AHook()
            {
                hookToRight = flipped
            });

            list.Add(new ADroneMove()
            {
                dir = -1
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
    }
}
