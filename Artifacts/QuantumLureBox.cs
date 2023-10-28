using JohannaTheTrucker.Cards;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Quantum Lure Box : (Boss) For every 3 shots (Attack cards) gain a Micro Missiles card
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Boss })]
    public class QuantumLureBox : Artifact
    {
        public int Counter;

        public override int? GetDisplayNumber(State s) => Counter == 0 ? null : Counter;

        public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
        {
            //check if card has attack action.
            if (!card.GetActions(state, combat).Any(e => e.GetType().IsAssignableTo(typeof(AAttack))))
                return;
            Counter++;
            if (Counter == 3)
            {
                Counter = 0;
                combat.Queue(new AAddCard()
                {
                    artifactPulse = Key(),
                    amount = 1,
                    card = new MicroMissiles(),
                    destination = CardDestination.Hand,
                });
            }
        }
    }
}