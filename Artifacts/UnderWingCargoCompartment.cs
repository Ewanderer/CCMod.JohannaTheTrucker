using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Under-Wing Cargo Compartment : Gain 1 extra energy per turn. Start the battle with 99 "Lose All Evade".
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Boss })]
    public class UnderWingCargoCompartment : Artifact
    {

        public override void OnReceiveArtifact(State state) => ++state.ship.baseEnergy;

        public override void OnRemoveArtifact(State state) => --state.ship.baseEnergy;

        public override void OnTurnStart(State state, Combat combat)
        {
            if (combat.turn == 1 || !combat.isPlayerTurn)
                return;
            if (state.ship.Get(Status.evade) > 0)
                combat.Queue(new AStatus()
                {
                    status = Status.evade,
                    artifactPulse = Key(),
                    targetPlayer = true,
                    statusAmount = 0,
                    mode = AStatusMode.Set
                });
        }

    }
}
