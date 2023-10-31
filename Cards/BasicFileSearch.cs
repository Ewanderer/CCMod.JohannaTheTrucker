﻿using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class BasicFileSearch : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
               
                case Upgrade.None:
                    result.Add(new ADrawCard()
					{
						amount = 2;
					}
					)
                case Upgrade.A:
					result.Add(new ADrawCard()
					{
						amount = 1;
					}
					)
                    
                    break;
                case Upgrade.B:
                    result.Add(new ADrawCard()
					{
						amount = 4;
					}
					)
                    break;
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = upgrade == upgrade.A? 0 : 1
            };
        }

        public override string Name() => "Basic File Search";
    }
}