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

        public void LoadManifest(ICustomEventHub eventHub)
        {
            //assign for local consumption
            _eventHub = eventHub;
            eventHub.MakeEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended");


            eventHub.MakeEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved");
            var harmony = new Harmony("JohannaTheTrucker.Artifacts");

            var amove_begin_method = typeof(AMove).GetMethod("Begin") ?? throw new Exception("AMove.Begin method not found.");

            var amove_begin_postfix = typeof(Manifest).GetMethod("AMove_Begin_Post", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Post method not found.");
            var amove_begin_prefix = typeof(Manifest).GetMethod("AMove_Begin_Pre", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Pre method not found.");

            harmony.Patch(amove_begin_method, postfix: new HarmonyMethod(amove_begin_postfix), prefix: new HarmonyMethod(amove_begin_prefix));

        }

        private static void AMove_Begin_Post(AMove __instance, int __state, State s, Combat c)
        {
            var new_pos = __instance.targetPlayer ? s.ship.x : c.otherShip.x;

            var distance = Math.Abs(new_pos - __state);
            if (distance > 0)
            {
                EventHub.SignalEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved", new(distance, __instance.targetPlayer, __instance.fromEvade, c, s));
            }
        }

        private static void AMove_Begin_Pre(AMove __instance, State s, Combat c, out int __state)
        {
            __state = __instance.targetPlayer ? s.ship.x : c.otherShip.x;
        }

        public void LoadManifest(IArtifactRegistry registry)
        {
            DecorativeSalmonArtifact = new ExternalArtifact(typeof(DecorativeSalmon), "JohannaTheTrucker.Artifacts.DecorativeSalmon", DecorativeSalmonSprite ?? throw new Exception("missing deco salom sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            DecorativeSalmonArtifact.AddLocalisation("en", "Decorative Salmon", "Every time a cluster is completely spent (not destroyed) gain 1 Shield");

            registry.RegisterArtifact(DecorativeSalmonArtifact);

            InertialEnginenArtifact = new ExternalArtifact(typeof(InertialEngine), "JohannaTheTrucker.Artifacts.InertialEngine", InertialEngineSprite ?? throw new Exception("missing InertialEngine sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            InertialEnginenArtifact.AddLocalisation("en", "Inertial Engine", "For every 6 spaces moved without using Evade, gain 1 Evade");

            registry.RegisterArtifact(InertialEnginenArtifact);
        }


    }

}
