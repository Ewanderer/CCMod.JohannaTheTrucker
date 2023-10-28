using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using FMOD;
using HarmonyLib;
using JohannaTheTrucker.Artifacts;
using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker
{
    public partial class Manifest
    {


        private static ICustomEventHub? _eventHub;

        internal static ICustomEventHub EventHub { get => _eventHub ?? throw new Exception(); set => _eventHub = value; }

        public static ExternalArtifact? DecorativeSalmonArtifact { get; private set; }
        public static ExternalArtifact? InertialEnginenArtifact { get; private set; }
        public static ExternalArtifact? LuckyLureArtifact { get; private set; }

        public void LoadManifest(ICustomEventHub eventHub)
        {
            //assign for local consumption
            _eventHub = eventHub;
            eventHub.MakeEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended");

            //distance, target_player, from_evade, combat, state
            eventHub.MakeEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved");
            //fired missile (missile, cluster missile, etc), hit_target, combat, state 
            eventHub.MakeEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying");

            var harmony = new Harmony("JohannaTheTrucker.Artifacts");

            var amove_begin_method = typeof(AMove).GetMethod("Begin") ?? throw new Exception("AMove.Begin method not found.");

            var amove_begin_postfix = typeof(InertialEngine).GetMethod("AMove_Begin_Post", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Post method not found.");
            var amove_begin_prefix = typeof(InertialEngine).GetMethod("AMove_Begin_Pre", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Pre method not found.");

            harmony.Patch(amove_begin_method, postfix: new HarmonyMethod(amove_begin_postfix), prefix: new HarmonyMethod(amove_begin_prefix));

            var amissilehit_begin_method = typeof(AMissileHit).GetMethod("Update") ?? throw new Exception("AMissileHit.Update method not found.");

            var amissilehit_update_postfix = typeof(LuckyLure).GetMethod("AMissileHit_Update_Post", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AMissileHit_Update_Post method not found.");
            var amissilehit_update_prefix = typeof(LuckyLure).GetMethod("AMissileHit_Update_Pre", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AMissileHit_Update_Pre method not found.");

            harmony.Patch(amissilehit_begin_method, prefix: new HarmonyMethod(amissilehit_update_prefix), postfix: new HarmonyMethod(amissilehit_update_postfix));
        }



        public void LoadManifest(IArtifactRegistry registry)
        {
            DecorativeSalmonArtifact = new ExternalArtifact(typeof(DecorativeSalmon), "JohannaTheTrucker.Artifacts.DecorativeSalmon", DecorativeSalmonSprite ?? throw new Exception("missing deco salom sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            DecorativeSalmonArtifact.AddLocalisation("en", "Decorative Salmon", "Every time a cluster is completely spent (not destroyed) gain 1 Shield");

            registry.RegisterArtifact(DecorativeSalmonArtifact);

            InertialEnginenArtifact = new ExternalArtifact(typeof(InertialEngine), "JohannaTheTrucker.Artifacts.InertialEngine", InertialEngineSprite ?? throw new Exception("missing InertialEngine sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            InertialEnginenArtifact.AddLocalisation("en", "Inertial Engine", "For every 6 spaces moved without using Evade, gain 1 Evade");

            registry.RegisterArtifact(InertialEnginenArtifact);

            LuckyLureArtifact = new ExternalArtifact(typeof(LuckyLure), "JohannaTheTrucker.Artifacts.LuckyLure", LuckyLureSprite ?? throw new Exception("missing LuckyLure sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            LuckyLureArtifact.AddLocalisation("en", "Lucky Lure", "For every 4 missiles that hit the enemy, gain 1 Midshift");

            registry.RegisterArtifact(LuckyLureArtifact);
        }


    }

}
