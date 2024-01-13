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
        public static ExternalArtifact? ExtraCargoPodArtifact { get; private set; }

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
                DecorativeSalmonArtifact = new ExternalArtifact("JohannaTheTrucker.Artifacts.DecorativeSalmon", typeof(DecorativeSalmon), DecorativeSalmonSprite ?? throw new Exception("missing deco salom sprite"),ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                DecorativeSalmonArtifact.AddLocalisation( "DECORATIVE SALMON", "Every time a cluster is completely spent (not destroyed) gain 1 <c=status>Shield.</c>");

                registry.RegisterArtifact(DecorativeSalmonArtifact);
            }
            {
                InertialEnginenArtifact = new ExternalArtifact("JohannaTheTrucker.Artifacts.InertialEngine", typeof(InertialEngine), InertialEngineSprite ?? throw new Exception("missing InertialEngine sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                InertialEnginenArtifact.AddLocalisation( "INERTIAL ENGINE", "For every 6 spaces moved without using <c=status>Evade</c>, gain 1 <c=status>Evade.</c>");

                registry.RegisterArtifact(InertialEnginenArtifact);
            }

            {
                LuckyLureArtifact = new ExternalArtifact("JohannaTheTrucker.Artifacts.LuckyLure", typeof(LuckyLure), LuckyLureSprite ?? throw new Exception("missing LuckyLure sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                LuckyLureArtifact.AddLocalisation( "LUCKY LURE", "For every 4 missiles that hit the enemy, gain 1 <c=status>Droneshift</c>");

                registry.RegisterArtifact(LuckyLureArtifact);
            }

            {
                FreebiesArtifact = new ExternalArtifact("JohannaTheTrucker.Artifacts.Freebies", typeof(Freebies), FreebiesSprite ?? throw new Exception("missing Freebies sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                FreebiesArtifact.AddLocalisation("FREEBIES!", "Launching or adding charges to clusters has a 50% chance to add 1 extra charge");

                registry.RegisterArtifact(FreebiesArtifact);
            }

            {
                QuantumLureBoxArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.QuantumLureBox", typeof(QuantumLureBox), QuantumLureBoxSprite ?? throw new Exception("missing QuantumLureBox sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                QuantumLureBoxArtifact.AddLocalisation("QUANTUM LURE BOX", "For every 3 shots (Attack cards) add a Micro Missiles to your hand.");

                registry.RegisterArtifact(QuantumLureBoxArtifact);
            }

            {
                ShieldBypassKeyArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.ShieldBypassKey", typeof(ShieldBypassKey), ShieldBypassKeySprite ?? throw new Exception("missing ShieldBypassKey sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                ShieldBypassKeyArtifact.AddLocalisation("SHIELD BYPASS KEY", "If you start your turn with at least 1 <c=status>shield</c>, <c=downside>lose 1</c> and gain 1 <c=status>evade.</c>");

                registry.RegisterArtifact(ShieldBypassKeyArtifact);
            }

            {
                AutolauncherArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.Autolauncher", typeof(Autolauncher), AutolauncherSprite ?? throw new Exception("missing Autolauncher sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                AutolauncherArtifact.AddLocalisation("AUTOLAUNCHER", "At the start of your turn, launch 1 cluster missile with 1 charge.");

                registry.RegisterArtifact(AutolauncherArtifact);
            }

            {
                MidrowProtectorProtocolArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.MidrowProtectorProtocol", typeof(MidrowProtectorProtocol), MidrowProtectorProtocolSprite ?? throw new Exception("missing MidrowProtectorProtocol sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                MidrowProtectorProtocolArtifact.AddLocalisation("MIDROW PROTECTOR PROTOCOL", "Gain 1 <c=status>bubbler</c> at the start of your turn if you don't have any. <c=downside>Start every battle with 99 Lose All Droneshift</c>.");

                registry.RegisterArtifact(MidrowProtectorProtocolArtifact);
            }

            {
                UnderWingCargoCompartmentArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.UnderWingCargoCompartment", typeof(UnderWingCargoCompartment), UnderWingCargoCompartmentSprite ?? throw new Exception("missing UnderWingCargoCompartment sprite"), ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                UnderWingCargoCompartmentArtifact.AddLocalisation("UNDER-WING POWER CORE", "Gain 1 extra energy per turn. <c=downside>Lose all evade at the start of your turns past the first.</c>");

                registry.RegisterArtifact(UnderWingCargoCompartmentArtifact);
            }

            {
                SalmonRoeArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.SalmonRoe", typeof(SalmonRoe), SalmonRoeSprite ?? throw new Exception("missing SalmonRoe sprite"),ownerDeck: Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."));

                SalmonRoeArtifact.AddLocalisation("SALMON ROE", "For every 3 missiles that miss, the next cluster gains 1 charge");

                registry.RegisterArtifact(SalmonRoeArtifact);
            }

            {
                ExtraCargoPodArtifact = new ExternalArtifact( "JohannaTheTrucker.Artifacts.ExtraCargoPodArtifact", typeof(ExtraCargoPod), ExtraCargoPodArtifactSprite ?? throw new Exception("missing ExtraCargoPod sprite"));

                ExtraCargoPodArtifact.AddLocalisation("EXTRA CARGO POD", "You're shown an extra card on non-boss battle rewards.");

                registry.RegisterArtifact(ExtraCargoPodArtifact);
            }

        }


    }

}
