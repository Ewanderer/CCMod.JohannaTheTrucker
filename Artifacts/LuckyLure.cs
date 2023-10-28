using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Artifacts
{
    /// <summary>
    /// Lucky Lure: For every 4 missiles that hit the enemy, gain 1 Midshift
    /// </summary>
    [ArtifactMeta(pools = new ArtifactPool[] { ArtifactPool.Common })]
    public class LuckyLure : Artifact
    {

        public LuckyLure()
        {
            Manifest.EventHub.ConnectToEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnAMissileHitDone);
        }

        public override void OnRemoveArtifact(State state)
        {
            Manifest.EventHub.DisconnectFromEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnAMissileHitDone);
        }

        public int counter;

        public override void OnCombatStart(State state, Combat combat)
        {
            counter = 0;
        }

        public override void OnCombatEnd(State state)
        {
            counter = 0;
        }

        private static void AMissileHit_Update_Pre(AMissileHit __instance, State s, Combat c, out Missile? __state)
        {
            //try and grab missile
            c.stuff.TryGetValue(__instance.worldX, out var stuffBase);
            if (stuffBase is Missile m && m.yAnimation >= 3.5)
                __state = m;
            else
                __state = null;
        }

        private static void AMissileHit_Update_Post(AMissileHit __instance, State s, Combat c, Missile? __state)
        {
            //check if this call is for an just fired missile
            if (__state == null)
                return;
            var was_miss = c.stuffOutro.LastOrDefault() == __state;

            Manifest.EventHub.SignalEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", new(__state, !was_miss, c, s));
        }

        private void OnAMissileHitDone(Tuple<StuffBase, bool, Combat, State> evt)
        {
            var was_hit = evt.Item2;
            if (!was_hit)
                return;
            var missile = evt.Item1;
            if (!missile.fromPlayer)
                return;
            if (!evt.Item4.artifacts.Contains(this))
            {
                Manifest.EventHub.DisconnectFromEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying", OnAMissileHitDone);
            }

            counter++;
            if (counter == 4)
            {
                counter = 0;
                evt.Item3.Queue(new AStatus()
                {
                    targetPlayer = true,
                    artifactPulse = this.Key(),
                    statusAmount = 1,
                    status = Status.droneShift
                });
            }


        }

    }
}
