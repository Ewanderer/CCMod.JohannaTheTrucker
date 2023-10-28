using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Autolauncher  At the start of turn, launch 1 cluster missile with 1 charge.
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class Autolauncher : Artifact
    {
        public override void OnTurnStart(State state, Combat combat)
        {
            if (!combat.isPlayerTurn)
                return;
            combat.Queue(new ASpawn()
            {
                artifactPulse = Key(),
                fromPlayer = true,
                thing = new ClusterMissile()
                {
                    fromPlayer = true,
                    stackSize = 1,
                    stackType = ClusterMissile.MissileType.normal,
                    targetPlayer = false,
                }
            });
        }
    }
}