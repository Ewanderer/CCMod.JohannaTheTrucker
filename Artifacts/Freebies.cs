using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Freebies!: (Boss) Launching or adding charges to clusters has a 50% chance to add 1 extra charge
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Boss })]
    public class Freebies : Artifact
    {
        public Freebies()
        {
            Manifest.EventHub.ConnectToEvent<Tuple<ClusterMissile, State>>("JohannaTheTrucker.ClusterMissileGrown", OnClusterMissileGrown);
        }

        public override void OnRemoveArtifact(State state)
        {
            Manifest.EventHub.DisconnectFromEvent<Tuple<ClusterMissile, State>>("JohannaTheTrucker.ClusterMissileGrown", OnClusterMissileGrown);
        }

        public override StuffBase ReplaceSpawnedThing(State state, Combat combat, StuffBase thing, bool spawnedByPlayer)
        {
            if (!spawnedByPlayer)
                return thing;
            if (thing is not ClusterMissile cm)
                return thing;
            if (state.rngActions.Next() >= 0.5)
            {
                cm.stackSize++;
                Pulse();
            }
            return cm;
        }

        private void OnClusterMissileGrown(Tuple<ClusterMissile, State> evt)
        {
            if (!evt.Item2.characters.SelectMany(e => e.artifacts).Concat(evt.Item2.artifacts).Contains(this))
            {
                Manifest.EventHub.DisconnectFromEvent<Tuple<ClusterMissile, State>>("JohannaTheTrucker.ClusterMissileGrown", OnClusterMissileGrown);
                return;
            }
            var cluster = evt.Item1;
            if (!cluster.fromPlayer)
                return;
            if (evt.Item2.rngActions.Next() >= 0.5)
            {
                cluster.stackSize++;
                Pulse();
            }
        }
    }
}