using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!state.artifacts.Contains(this))
            {
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
