using JohannaTheTrucker.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Quantum Lure Box : (Boss) For every 3 shots (Attack cards) gain a Micro Missiles card
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Boss })]
    public class QuantumLureBox :Artifact
    {
        public int Counter;

        public override void OnCombatStart(State state, Combat combat)
        {
            Counter = 0;
        }

        public override void OnCombatEnd(State state)
        {
            Counter = 0;
        }

        public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
        {
            //check if card has attack action.
            if (!card.GetActions(state, combat).Any(e => e.GetType().IsAssignableTo(typeof(AAttack))))
                return;
            Counter++;
            if (Counter == 3) {
                Counter = 0;
                combat.Queue(new AAddCard()
                {
                    artifactPulse=Key(),
                    amount=1,
                    card=new MicroMissiles(),
                    destination=CardDestination.Hand,
                });
            }
        }
    }
}
