using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Decorative Salmon: Every time a cluster is completely spent (not destroyed) gain 1 Shield
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class DecorativeSalmon : Artifact
    {
        public DecorativeSalmon()
        {
            Manifest.EventHub.ConnectToEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended", OnClusterMissileExpended);
        }

        public override void OnRemoveArtifact(State state)
        {
            Manifest.EventHub.DisconnectFromEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended", OnClusterMissileExpended);
        }

        private void OnClusterMissileExpended(Tuple<ClusterMissile, Combat, State> evt)
        {
            var clusterMissile = evt.Item1;
            var combat = evt.Item2;
            var state = evt.Item3;
            //check if this artifact is valid.
            if (!state.artifacts.Contains(this))
            {
                //make sure cleanup is only performed once.
                Manifest.EventHub.DisconnectFromEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended", OnClusterMissileExpended);
                return;
            }
            if (!clusterMissile.fromPlayer)
                return;
            combat.QueueImmediate(new AStatus()
            {
                artifactPulse = Key(),
                targetPlayer = true,
                statusAmount = 1,
                status = Status.shield
            });
        }
    }
}