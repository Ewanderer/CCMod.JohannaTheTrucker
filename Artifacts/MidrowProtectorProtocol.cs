namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Midrow Protector Protocol: If you don't have any bubbler, gain 1 bubbler at the start of turn. Start the battle with 99 "Lose All Midshift". (Isaac has an artifact that grants Bubbler after playing 4 of his own cards, this is the same buff)
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Boss })]
    public class MidrowProtectorProtocol : Artifact
    {
        public override void OnCombatStart(State state, Combat combat)
        {
            combat.Queue(new AStatus()
            {
                mode = AStatusMode.Set,
                statusAmount = 100,
                targetPlayer = true,
                artifactPulse = Key(),
                status = (Status)(Manifest.LoseDroneShiftStatus?.Id ?? throw new Exception("LoseDroneShiftStatus missing."))
            });
        }

        public override void OnTurnStart(State state, Combat combat)
        {
            if (!combat.isPlayerTurn)
                return;
            if (state.ship.Get(Status.bubbleJuice) == 0)
            {
                combat.QueueImmediate(new AStatus()
                {
                    status = Status.bubbleJuice,
                    statusAmount = 1,
                    targetPlayer = true,
                    artifactPulse = Key(),
                });
            }
        }
    }
}