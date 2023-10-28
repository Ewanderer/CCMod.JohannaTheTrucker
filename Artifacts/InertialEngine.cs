namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Inertial Engine: For every 6 spaces moved without using Evade, gain 1 Evade
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class InertialEngine : Artifact
    {
        public int counter;

        public InertialEngine()
        {
            Manifest.EventHub.ConnectToEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved", ShipMoved);
        }

        public override int? GetDisplayNumber(State s) => counter == 0 ? null : counter;

        public override void OnRemoveArtifact(State state)
        {
            Manifest.EventHub.DisconnectFromEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved", ShipMoved);
        }

        private static void AMove_Begin_Post(AMove __instance, int __state, State s, Combat c)
        {
            var new_pos = __instance.targetPlayer ? s.ship.x : c.otherShip.x;

            var distance = Math.Abs(new_pos - __state);
            if (distance > 0)
            {
                Manifest.EventHub.SignalEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved", new(distance, __instance.targetPlayer, __instance.fromEvade, c, s));
            }
        }

        private static void AMove_Begin_Pre(AMove __instance, State s, Combat c, out int __state)
        {
            __state = __instance.targetPlayer ? s.ship.x : c.otherShip.x;
        }

        private void ShipMoved(Tuple<int, bool, bool, Combat, State> evt)
        {
            var distance = evt.Item1;
            var target_player = evt.Item2;
            var from_evade = evt.Item3;
            var combat = evt.Item4;
            var state = evt.Item5;

            if (!state.characters.SelectMany(e => e.artifacts).Concat(state.artifacts).Contains(this))
            {
                //make sure cleanup is only performed once.
                Manifest.EventHub.DisconnectFromEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved", ShipMoved);
                return;
            }

            if (from_evade)
                return;
            if (!target_player)
                return;
            counter += distance;
            if (counter >= 6)
            {
                var ammount = counter / 6;
                counter = counter - (ammount * 6);
                combat.QueueImmediate(new AStatus()
                {
                    targetPlayer = true,
                    statusAmount = ammount,
                    status = Status.evade,
                    artifactPulse = Key(),
                });
            }
        }
    }
}