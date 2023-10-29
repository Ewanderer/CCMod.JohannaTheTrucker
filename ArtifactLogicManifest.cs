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
        public static ExternalArtifact? FreebiesArtifact { get; private set; }
        public static ExternalArtifact? QuantumLureBoxArtifact { get; private set; }
        public static ExternalArtifact? ShieldBypassKeyArtifact { get; private set; }
        public static ExternalArtifact? AutolauncherArtifact { get; private set; }
        public static ExternalArtifact? MidrowProtectorProtocolArtifact { get; private set; }
        public static ExternalArtifact? UnderWingCargoCompartmentArtifact { get; private set; }
        public static ExternalArtifact? SalmonRoeArtifact { get; private set; }

        public void LoadManifest(ICustomEventHub eventHub)
        {
            //assign for local consumption
            _eventHub = eventHub;
            eventHub.MakeEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended");

            //distance, target_player, from_evade, combat, state
            eventHub.MakeEvent<Tuple<int, bool, bool, Combat, State>>("JohannaTheTrucker.ShipMoved");
            //fired missile (missile, cluster missile, etc), hit_target, combat, state 
            eventHub.MakeEvent<Tuple<StuffBase, bool, Combat, State>>("JohannaTheTrucker.MissileFlying");

            eventHub.MakeEvent<Tuple<ClusterMissile, State>>("JohannaTheTrucker.ClusterMissileGrown");

            var harmony = new Harmony("JohannaTheTrucker.Artifacts");
            {
                var amove_begin_method = typeof(AMove).GetMethod("Begin") ?? throw new Exception("AMove.Begin method not found.");

                var amove_begin_postfix = typeof(InertialEngine).GetMethod("AMove_Begin_Post", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Post method not found.");
                var amove_begin_prefix = typeof(InertialEngine).GetMethod("AMove_Begin_Pre", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("Manifest.AMove_Begin_Pre method not found.");

                harmony.Patch(amove_begin_method, postfix: new HarmonyMethod(amove_begin_postfix), prefix: new HarmonyMethod(amove_begin_prefix));
            }
            {
                var amissilehit_begin_method = typeof(AMissileHit).GetMethod("Update") ?? throw new Exception("AMissileHit.Update method not found.");

                var amissilehit_update_postfix = typeof(LuckyLure).GetMethod("AMissileHit_Update_Post", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AMissileHit_Update_Post method not found.");
                var amissilehit_update_prefix = typeof(LuckyLure).GetMethod("AMissileHit_Update_Pre", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new Exception("AMissileHit_Update_Pre method not found.");

                harmony.Patch(amissilehit_begin_method, prefix: new HarmonyMethod(amissilehit_update_prefix), postfix: new HarmonyMethod(amissilehit_update_postfix));
            }
        }



        public void LoadManifest(IArtifactRegistry registry)
        {
            {
                DecorativeSalmonArtifact = new ExternalArtifact(typeof(DecorativeSalmon), "JohannaTheTrucker.Artifacts.DecorativeSalmon", DecorativeSalmonSprite ?? throw new Exception("missing deco salom sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                DecorativeSalmonArtifact.AddLocalisation("en", "DECORATIVE SALMON", "Every time a cluster is completely spent (not destroyed) gain 1 <c=status>Shield.</c>");

                registry.RegisterArtifact(DecorativeSalmonArtifact);
            }
            {
                InertialEnginenArtifact = new ExternalArtifact(typeof(InertialEngine), "JohannaTheTrucker.Artifacts.InertialEngine", InertialEngineSprite ?? throw new Exception("missing InertialEngine sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                InertialEnginenArtifact.AddLocalisation("en", "INERTIAL ENGINE", "For every 6 spaces moved without using <c=status>Evade</c>, gain 1 <c=status>Evade.</c>");

                registry.RegisterArtifact(InertialEnginenArtifact);
            }

            {
                LuckyLureArtifact = new ExternalArtifact(typeof(LuckyLure), "JohannaTheTrucker.Artifacts.LuckyLure", LuckyLureSprite ?? throw new Exception("missing LuckyLure sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                LuckyLureArtifact.AddLocalisation("en", "LUCKY LURE", "For every 4 missiles that hit the enemy, gain 1 <c=status>Midshift</c>");

                registry.RegisterArtifact(LuckyLureArtifact);
            }

            {
                FreebiesArtifact = new ExternalArtifact(typeof(Freebies), "JohannaTheTrucker.Artifacts.Freebies", FreebiesSprite ?? throw new Exception("missing Freebies sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                FreebiesArtifact.AddLocalisation("en", "FREEBIES!", "Launching or adding charges to clusters has a 50% chance to add 1 extra charge");

                registry.RegisterArtifact(FreebiesArtifact);
            }

            {
                QuantumLureBoxArtifact = new ExternalArtifact(typeof(QuantumLureBox), "JohannaTheTrucker.Artifacts.QuantumLureBox", QuantumLureBoxSprite ?? throw new Exception("missing QuantumLureBox sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                QuantumLureBoxArtifact.AddLocalisation("en", "QUANTUM LURE BOX", "For every 3 shots (Attack cards) add a Micro Missiles to your hand.");

                registry.RegisterArtifact(QuantumLureBoxArtifact);
            }

            {
                ShieldBypassKeyArtifact = new ExternalArtifact(typeof(ShieldBypassKey), "JohannaTheTrucker.Artifacts.ShieldBypassKey", ShieldBypassKeySprite ?? throw new Exception("missing ShieldBypassKey sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                ShieldBypassKeyArtifact.AddLocalisation("en", "SHIELD BYPASS KEY", "If you start your turn with at least 1 <c=status>shield</c>, <c=downside>lose 1</c> and gain 1 <c=status>evade.</c>");

                registry.RegisterArtifact(ShieldBypassKeyArtifact);
            }

            {
                AutolauncherArtifact = new ExternalArtifact(typeof(Autolauncher), "JohannaTheTrucker.Artifacts.Autolauncher", AutolauncherSprite ?? throw new Exception("missing Autolauncher sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                AutolauncherArtifact.AddLocalisation("en", "AUTOLAUNCHER", "At the start of your turn, launch 1 cluster missile with 1 charge.");

                registry.RegisterArtifact(AutolauncherArtifact);
            }

            {
                MidrowProtectorProtocolArtifact = new ExternalArtifact(typeof(MidrowProtectorProtocol), "JohannaTheTrucker.Artifacts.MidrowProtectorProtocol", MidrowProtectorProtocolSprite ?? throw new Exception("missing MidrowProtectorProtocol sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                MidrowProtectorProtocolArtifact.AddLocalisation("en", "MIDROW PROTECTOR PROTOCOL", "Gain 1 <c=status>bubbler</c> at the start of your turn if you don't have any. <c=downside>Start every battle with 99 Lose All Midshift</c>.");

                registry.RegisterArtifact(MidrowProtectorProtocolArtifact);
            }

            {
                UnderWingCargoCompartmentArtifact = new ExternalArtifact(typeof(UnderWingCargoCompartment), "JohannaTheTrucker.Artifacts.UnderWingCargoCompartment", UnderWingCargoCompartmentSprite ?? throw new Exception("missing UnderWingCargoCompartment sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                UnderWingCargoCompartmentArtifact.AddLocalisation("en", "UNDER-WING CARGO COMPARTMENT", "Gain 1 extra energy per turn. <c=downside>Lose all evade at the start of your turns past the first.</c>");

                registry.RegisterArtifact(UnderWingCargoCompartmentArtifact);
            }

            {
                SalmonRoeArtifact = new ExternalArtifact(typeof(SalmonRoe), "JohannaTheTrucker.Artifacts.SalmonRoe", SalmonRoeSprite ?? throw new Exception("missing SalmonRoe sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

                SalmonRoeArtifact.AddLocalisation("en", "SALMON ROE", "For every 3 missiles that miss, the next cluster gains 1 charge");

                registry.RegisterArtifact(SalmonRoeArtifact);
            }

        }


    }

}
