using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Shield Bypass Key: At the start of turn, lose 1 shield to gain 1 evade. (If you can't lose 1 shield you don't gain the other effect)
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class ShieldBypassKey : Artifact
    {
        public override void OnTurnStart(State state, Combat combat)
        {
            if (!combat.isPlayerTurn)
                return;
            if (state.ship.Get(Status.shield) > 0)
            {
                combat.QueueImmediate(new AStatus()
                {
                    status = Status.evade,
                    statusAmount = 1,
                    artifactPulse = Key(),
                    targetPlayer = true,
                    mode = AStatusMode.Add
                });

                var a = new AStatus()
                {
                    status = Status.shield,
                    statusAmount = -1,
                    artifactPulse = Key(),
                    targetPlayer = true,
                    mode = AStatusMode.Add
                };
                //Add small delay.
                a.timer += 0.15;
                combat.QueueImmediate(a);
            }
        }
    }
}
