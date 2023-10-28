using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Salmon Roe: For every 3 missiles that miss, the next cluster gains 1 charge
    /// </summary>
    public class SalmonRoe : Artifact
    {
        public int counter;

        public SalmonRoe()
        {
            Manifest.EventHub.ConnectToEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnMissileFlying);
        }

        public override int? GetDisplayNumber(State s) => counter == 0 ? null : counter;

        public override void OnRemoveArtifact(State state)
        {
            Manifest.EventHub.DisconnectFromEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnMissileFlying);
        }

        public override StuffBase ReplaceSpawnedThing(State state, Combat combat, StuffBase thing, bool spawnedByPlayer)
        {
            if (!spawnedByPlayer)
                return thing;
            if (thing is not ClusterMissile cluster)
                return thing;
            var extra_stacks = counter / 3;
            cluster.stackSize += extra_stacks;
            counter -= extra_stacks * 3;
            return cluster;
        }

        private void OnMissileFlying(Tuple<StuffBase, bool, Combat, State> evt)
        {
            var was_hit = evt.Item2;
            if (was_hit)
                return;
            var missile = evt.Item1;
            if (!missile.fromPlayer)
                return;
            if (!evt.Item4.artifacts.Contains(this))
            {
                Manifest.EventHub.DisconnectFromEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnMissileFlying);
            }
            counter++;
        }
    }
}