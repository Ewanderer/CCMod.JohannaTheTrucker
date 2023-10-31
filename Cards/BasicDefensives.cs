﻿using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(deck =Deck.colorless, rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B },dontOffer =true)]
    public class BasicDefensives : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();
            switch (upgrade)
            {
               
                case Upgrade.None:
                    result.Add(new AStatus() { 
						status=Status.evade,
						statusAmount=1,
						targetPlayer=true
                    });
                    result.Add(new AStatus()
                    {
                        status=Status.shield, 
                        statusAmount=1,
                        targetPlayer = true
                    });
                    break;
		case Upgrade.A:
                    result.Add(new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true
                    });
                    result.Add(new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 2,
                        targetPlayer = true
                    });
	    	break;
                case Upgrade.B:
					result.Add(new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true
                    });
                    result.Add(new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true
                    });
	    	break;
                
            }
            return result;
        }

        public override CardData GetData(State state)
        {
            return new CardData
            {
                cost = 2,
            };
        }

        public override string Name() => "Basic Defensives";
    }
}
