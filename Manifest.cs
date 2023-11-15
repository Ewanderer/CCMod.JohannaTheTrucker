using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using HarmonyLib;
using JohannaTheTrucker.Actions;
using JohannaTheTrucker.Cards;
using System.Reflection;

namespace JohannaTheTrucker
{
    public partial class Manifest : ISpriteManifest, IDeckManifest, IGlossaryManifest, ICardManifest, ICharacterManifest, IAnimationManifest, IStatusManifest, ICustomEventManifest, IArtifactManifest, IShipPartManifest, IShipManifest, IStartershipManifest, IModManifest
    {

        public static ExternalSprite? Ship_Bay_Sprite { get; private set; }
        public static ExternalSprite? Ship_Cannon_Sprite { get; private set; }
        public static ExternalSprite? Ship_Cargo_Right_Sprite { get; private set; }
        public static ExternalSprite? Ship_Chassis_Sprite { get; private set; }
        public static ExternalSprite? Ship_Cockpit_Sprite { get; private set; }
        public static ExternalSprite? Ship_Hull_Sprite { get; private set; }
        public static ExternalSprite? Ship_Cargo_Left_Sprite { get; private set; }
        public static ExternalSprite? Ship_Scaffholding_Sprite { get; private set; }
        public static ExternalSprite? ExtraCargoPodArtifactSprite { get; private set; }

        public static ExternalGlossary? AHook_Glossary { get; private set; }
        public static ExternalGlossary? ClusterMissile_Glossary { get; private set; }
        public static ExternalGlossary? ClusterMissileHE_Glossary { get; private set; }
        public static ExternalGlossary? ClusterMissileSeeker_Glossary { get; private set; }
        public static ExternalGlossary? ClusterMissileHESeeker_Glossary { get; private set; }
        public static ExternalGlossary? AFlipMidrow_Glossary { get; private set; }
        public static ExternalSprite? ClusterMissleIcon { get; private set; }
        public static ExternalSprite? ClusterMissleToken { get; private set; }
        public static ExternalSprite? SmartExplosiveSprite { get; private set; }
        public static ExternalSprite? RocketSiloSprite { get; private set; }
        public static ExternalSprite? GrowClusterSprite { get; private set; }
        public static ExternalSprite? PopBubblesSprite { get; private set; }
        public static ExternalSprite? LoseDroneShiftSprite { get; private set; }
        public static ExternalCard? ClusterRocketCard { get; private set; }
        public static ExternalSprite? HEClusterMissleIcon { get; private set; }
        public static ExternalSprite? HEClusterMissleToken { get; private set; }
        public static ExternalSprite? HESeekerClusterMissleToken { get; private set; }
        public static ExternalSprite? HookIcon { get; private set; }
        public static ExternalSprite? HookLeftIcon { get; private set; }
        public static ExternalSprite? HookRightIcon { get; private set; }
        public static ExternalSprite? JohannaCardFrame { get; private set; }
        public static ExternalSprite? JohannaUncommonCardFrame { get; private set; }
        public static ExternalSprite? JohannaRareCardFrame { get; private set; }
        public static ExternalSprite? JohannaPanelFrame { get; private set; }
        public static ExternalSprite? DecorativeSalmonSprite { get; private set; }
        public static ExternalSprite? InertialEngineSprite { get; private set; }
        public static ExternalSprite? LuckyLureSprite { get; private set; }
        public static ExternalSprite? FreebiesSprite { get; private set; }
        public static ExternalSprite? QuantumLureBoxSprite { get; private set; }
        public static ExternalSprite? ShieldBypassKeySprite { get; private set; }
        public static ExternalSprite? SalmonRoeSprite { get; private set; }
        public static ExternalSprite? AutolauncherSprite { get; private set; }
        public static ExternalSprite? MidrowProtectorProtocolSprite { get; private set; }
        public static ExternalSprite? UnderWingCargoCompartmentSprite { get; private set; }
        public static IEnumerable<ExternalSprite>? HookFxSprites { get; private set; }
        public static ExternalCharacter? JohannaCharacter { get; private set; }
        public static ExternalDeck? JohannaDeck { get; private set; }
        public static ExternalAnimation? JohannaDefaultAnimation { get; private set; }
        public static ExternalSprite? JohannaMini { get; private set; }
        public static ExternalAnimation? JohannaMiniAnimation { get; private set; }
        public static ExternalAnimation? TalkAngryAnimation { get; private set; }
        public static ExternalAnimation? TalkLaughAnimation { get; private set; }
        public static ExternalAnimation? TalkNeutralAnimation { get; private set; }
        public static ExternalAnimation? TalkReminisceAnimation { get; private set; }
        public static ExternalAnimation? TalkSadAnimation { get; private set; }
        public static ExternalAnimation? TalkScaredAnimation { get; private set; }
        public static ExternalAnimation? GameoverAnimation { get; private set; }
        public static ExternalSprite? JohannaPotrait { get; private set; }
        public static ExternalCard? ReelInCard { get; private set; }
        public static ExternalCard? SaturationFireCard { get; private set; }
        public static ExternalCard? ShiftClusterCard { get; private set; }
        public static ExternalCard? HEClusterCard { get; private set; }
        public static ExternalCard? SeekingClusterCard { get; private set; }
        public static ExternalCard? LeapFrogCard { get; private set; }
        public static ExternalCard? SmallManeuverCard { get; private set; }
        public static ExternalCard? DoubleHookCard { get; private set; }
        public static ExternalCard? VarietyPackCard { get; private set; }
        public static ExternalCard? SpaceFoldingCard { get; private set; }
        public static ExternalCard? EnemyShiftCard { get; private set; }
        public static ExternalCard? ReplicatorCard { get; private set; }
        public static ExternalCard? MicroMissilesCard { get; private set; }
        public static ExternalCard? FireFireFireCard { get; private set; }
        public static ExternalCard? ReadjustCard { get; private set; }
        public static ExternalCard? RocketSiloCard { get; private set; }
        public static ExternalCard? MultiplicityCard { get; private set; }
        public static ExternalCard? MassUpgradeCard { get; private set; }
        public static ExternalCard? ReboundCard { get; private set; }
        public static ExternalCard? SmartExplosivesCard { get; private set; }
        public static ExternalCard? BigSwingCard { get; private set; }
        public static ExternalCard? EngineStallCard { get; private set; }
        public static ExternalCard? BasicDefensivesCard { get; private set; }
        public static ExternalCard? BasicFileSearchCard { get; private set; }
        public static ExternalSprite? SeekerClusterMissleIcon { get; private set; }
        public static ExternalSprite? SeekerClusterMissleToken { get; private set; }
        public static ExternalSprite? FoldingCardSprite { get; private set; }
        public static ExternalSprite? HookCardSprite { get; private set; }
        public static ExternalSprite? MissileCardSprite { get; private set; }
        public static ExternalSprite? StallCardSprite { get; private set; }

        public static ExternalGlossary? AGrowClusterGlossary { get; private set; }


        public static List<ExternalSprite> TalkAngrySprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkLaughSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkNeutralSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkReminisceSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkSadSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkScaredSprites { get; private set; } = new List<ExternalSprite>();

        public DirectoryInfo? ModRootFolder { get; set; }
        public string Name { get; init; } = "Actionmartini.JohannaTheTrucker";
        public DirectoryInfo? GameRootFolder { get; set; }

        public IEnumerable<string> Dependencies => new string[0];

        void ISpriteManifest.LoadManifest(IArtRegistry artRegistry)
        {
            if (ModRootFolder == null)
                throw new Exception("Root Folder not set");

            //ship parts

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisBay.png"));
                Ship_Bay_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisBay", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Bay_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisCannon.png"));
                Ship_Cannon_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisCannon", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Cannon_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisCargoLeft.png"));
                Ship_Cargo_Left_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisCargoLeft", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Cargo_Left_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisCargoRight.png"));
                Ship_Cargo_Right_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisCargoRight", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Cargo_Right_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisChassis.png"));
                Ship_Chassis_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisChassis", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Chassis_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisHull.png"));
                Ship_Hull_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisHull", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Hull_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisScaffholding.png"));
                Ship_Scaffholding_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisScaffholding", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Scaffholding_Sprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "UrsaeMajoris", Path.GetFileName("UrsaeMajorisCockpit.png"));
                Ship_Cockpit_Sprite = new ExternalSprite("JohannaTheTrucker.sprites.UrsaeMajorisCockpit", new FileInfo(path));
                artRegistry.RegisterArt(Ship_Cockpit_Sprite);
            }


            //artifacts

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("decorative_salmon.png"));
                DecorativeSalmonSprite = new ExternalSprite("JohannaTheTrucker.decorative_salmon", new FileInfo(path));
                artRegistry.RegisterArt(DecorativeSalmonSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("intertial_engine.png"));
                InertialEngineSprite = new ExternalSprite("JohannaTheTrucker.intertial_engine", new FileInfo(path));
                artRegistry.RegisterArt(InertialEngineSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("lucky_lore.png"));
                LuckyLureSprite = new ExternalSprite("JohannaTheTrucker.lucky_lore", new FileInfo(path));
                artRegistry.RegisterArt(LuckyLureSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("freebies.png"));
                FreebiesSprite = new ExternalSprite("JohannaTheTrucker.freebies", new FileInfo(path));
                artRegistry.RegisterArt(FreebiesSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("quantum_lure_box.png"));
                QuantumLureBoxSprite = new ExternalSprite("JohannaTheTrucker.quantum_lure_box", new FileInfo(path));
                artRegistry.RegisterArt(QuantumLureBoxSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("shield_bypass_key.png"));
                ShieldBypassKeySprite = new ExternalSprite("JohannaTheTrucker.shield_bypass_key", new FileInfo(path));
                artRegistry.RegisterArt(ShieldBypassKeySprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("autolauncher.png"));
                AutolauncherSprite = new ExternalSprite("JohannaTheTrucker.autolauncher", new FileInfo(path));
                artRegistry.RegisterArt(AutolauncherSprite);
            }


            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("midrow_protector_protocol.png"));
                MidrowProtectorProtocolSprite = new ExternalSprite("JohannaTheTrucker.midrow_protector_protocol", new FileInfo(path));
                artRegistry.RegisterArt(MidrowProtectorProtocolSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("underwing_cargo_compartment.png"));
                UnderWingCargoCompartmentSprite = new ExternalSprite("JohannaTheTrucker.underwing_cargo_compartment", new FileInfo(path));
                artRegistry.RegisterArt(UnderWingCargoCompartmentSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("salmon_roe.png"));
                SalmonRoeSprite = new ExternalSprite("JohannaTheTrucker.salmon_roe", new FileInfo(path));
                artRegistry.RegisterArt(SalmonRoeSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", "artifact_icons", Path.GetFileName("extra_cargo_pod.png"));
                ExtraCargoPodArtifactSprite = new ExternalSprite("JohannaTheTrucker.extra_cargo_pod", new FileInfo(path));
                artRegistry.RegisterArt(ExtraCargoPodArtifactSprite);
            }

            //load the character sprite

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JohannaDefault.png"));
                JohannaPotrait = new ExternalSprite("JohannaTheTrucker.JohannaPotrait", new FileInfo(path));
                artRegistry.RegisterArt(JohannaPotrait);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JohannaMini.png"));
                JohannaMini = new ExternalSprite("JohannaTheTrucker.JohannaMini", new FileInfo(path));
                artRegistry.RegisterArt(JohannaMini);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoFrame.png"));
                JohannaPanelFrame = new ExternalSprite("JohannaTheTrucker.JohannaPanelFrame", new FileInfo(path));
                artRegistry.RegisterArt(JohannaPanelFrame);
            }

            //hook sprite

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("HookIcon.png"));
                HookIcon = new ExternalSprite("JohannaTheTrucker.HookIcon", new FileInfo(path));
                artRegistry.RegisterArt(HookIcon);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("HookLeftIcon.png"));
                HookLeftIcon = new ExternalSprite("JohannaTheTrucker.HookLeftIcon", new FileInfo(path));
                artRegistry.RegisterArt(HookLeftIcon);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("HookRightIcon.png"));
                HookRightIcon = new ExternalSprite("JohannaTheTrucker.HookRightIcon", new FileInfo(path));
                artRegistry.RegisterArt(HookRightIcon);
            }



            //cluster missile sprite

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ClusterMissile.png"));
                ClusterMissleToken = new ExternalSprite("JohannaTheTrucker.ClusterMissleToken", new FileInfo(path));
                artRegistry.RegisterArt(ClusterMissleToken);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ClusterMissile_HE.png"));
                HEClusterMissleToken = new ExternalSprite("JohannaTheTrucker.HEClusterMissleToken", new FileInfo(path));
                artRegistry.RegisterArt(HEClusterMissleToken);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ClusterMissile_Seeker.png"));
                SeekerClusterMissleToken = new ExternalSprite("JohannaTheTrucker.SeekerClusterMissleToken", new FileInfo(path));
                artRegistry.RegisterArt(SeekerClusterMissleToken);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ClusterMissile_HE_seeker.png"));
                HESeekerClusterMissleToken = new ExternalSprite("JohannaTheTrucker.HESeekerClusterMissleToken", new FileInfo(path));
                artRegistry.RegisterArt(HESeekerClusterMissleToken);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("cm.png"));
                ClusterMissleIcon = new ExternalSprite("JohannaTheTrucker.ClusterMissleIcon", new FileInfo(path));
                artRegistry.RegisterArt(ClusterMissleIcon);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("scm.png"));
                SeekerClusterMissleIcon = new ExternalSprite("JohannaTheTrucker.SeekerClusterMissleIcon", new FileInfo(path));
                artRegistry.RegisterArt(SeekerClusterMissleIcon);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("hcm.png"));
                HEClusterMissleIcon = new ExternalSprite("JohannaTheTrucker.HEClusterMissleIcon", new FileInfo(path));
                artRegistry.RegisterArt(HEClusterMissleIcon);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("smart_explosive.png"));
                SmartExplosiveSprite = new ExternalSprite("JohannaTheTrucker.SmartExlposiveSprite", new FileInfo(path));
                artRegistry.RegisterArt(SmartExplosiveSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("rocket_silo.png"));
                RocketSiloSprite = new ExternalSprite("JohannaTheTrucker.RocketSiloSprite", new FileInfo(path));
                artRegistry.RegisterArt(RocketSiloSprite);
            }
            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("clear_bubble.png"));
                PopBubblesSprite = new ExternalSprite("JohannaTheTrucker.PopBubbleSprite", new FileInfo(path));
                artRegistry.RegisterArt(PopBubblesSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("grow_cluster.png"));
                GrowClusterSprite = new ExternalSprite("JohannaTheTrucker.GrowClusterSprite", new FileInfo(path));
                artRegistry.RegisterArt(GrowClusterSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites/action_icons", Path.GetFileName("lose_droneshift_status.png"));
                LoseDroneShiftSprite = new ExternalSprite("JohannaTheTrucker.LoseDroneShiftSprite", new FileInfo(path));
                artRegistry.RegisterArt(LoseDroneShiftSprite);
            }

            // deck sprite

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardFrame.png"));
                JohannaCardFrame = new ExternalSprite("JohannaTheTrucker.JohannaDeckFrame", new FileInfo(path));
                artRegistry.RegisterArt(JohannaCardFrame);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardFrameRA.png"));
                JohannaRareCardFrame = new ExternalSprite("JohannaTheTrucker.JohannaRareDeckFrame", new FileInfo(path));
                artRegistry.RegisterArt(JohannaRareCardFrame);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardFrameUC.png"));
                JohannaUncommonCardFrame = new ExternalSprite("JohannaTheTrucker.JohannaUncommonDeckFrame", new FileInfo(path));
                artRegistry.RegisterArt(JohannaUncommonCardFrame);
            }
            //card sprites
            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardArtHook.png"));
                HookCardSprite = new ExternalSprite("JohannaTheTrucker.HookCardSprite", new FileInfo(path));
                artRegistry.RegisterArt(HookCardSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardArtMissiles.png"));
                MissileCardSprite = new ExternalSprite("JohannaTheTrucker.MissileCardSprite", new FileInfo(path));
                artRegistry.RegisterArt(MissileCardSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardArtFolding.png"));
                FoldingCardSprite = new ExternalSprite("JohannaTheTrucker.FoldingCardSprite", new FileInfo(path));
                artRegistry.RegisterArt(FoldingCardSprite);
            }

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("JoCardArtStall.png"));
                StallCardSprite = new ExternalSprite("JohannaTheTrucker.StallCardSprite", new FileInfo(path));
                artRegistry.RegisterArt(StallCardSprite);
            }

            //hook fx animation
            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_angry");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = new ExternalSprite("JohannaTheTrucker.TalkAngry" + i, files[i]);
                    TalkAngrySprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }

            //talk animations
            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "hook_fx");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                var list = new List<ExternalSprite>();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = new ExternalSprite("JohannaTheTrucker.HookFxSprite" + i, files[i]);
                    list.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
                HookFxSprites = list;
            }

            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_laugh");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = new ExternalSprite("JohannaTheTrucker.TalkLaugh" + i, files[i]);
                    TalkLaughSprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }

            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_neutral");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = new ExternalSprite("JohannaTheTrucker.TalkNeutral" + i, files[i]);
                    TalkNeutralSprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }

            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_reminisce");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = (new ExternalSprite("JohannaTheTrucker.TalkReminisce" + i, files[i]));
                    TalkReminisceSprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }

            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_sad");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = (new ExternalSprite("JohannaTheTrucker.TalkSad" + i, files[i]));
                    TalkSadSprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }

            {
                var dir_path = Path.Combine(ModRootFolder.FullName, "Sprites", "talk_scared");
                var files = Directory.GetFiles(dir_path).Select(e => new FileInfo(e)).ToArray();
                for (int i = 0; i < files.Length; i++)
                {
                    var spr = (new ExternalSprite("JohannaTheTrucker.TalkScared" + i, files[i]));
                    TalkScaredSprites.Add(spr);
                    artRegistry.RegisterArt(spr);
                }
            }


        }

        private static System.Drawing.Color Johanna_Primary_Color = System.Drawing.Color.FromArgb(65, 144, 195);

        public void LoadManifest(IDeckRegistry registry)
        {
            ExternalSprite cardArtDefault = ExternalSprite.GetRaw((int)Spr.cards_colorless);
            ExternalSprite borderSprite = JohannaCardFrame ?? throw new Exception();
            JohannaDeck = new ExternalDeck(
                "JohannaTheTrucker.JohannaDeck",
              Johanna_Primary_Color,
                System.Drawing.Color.White,
                cardArtDefault,
                borderSprite,
                null);
            registry.RegisterDeck(JohannaDeck);
        }

        void IGlossaryManifest.LoadManifest(IGlossaryRegisty registry)
        {
            AHook_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.AHookDesc", "JohannaTheTruckerAHook", false, ExternalGlossary.GlossayType.action, HookIcon ?? throw new Exception("Miossing Hook Icon"));
            AHook_Glossary.AddLocalisation("en", "Hookshot", "Align ship's missile bay with the closest midrow object", null);
            registry.RegisterGlossary(AHook_Glossary);

            AGrowClusterGlossary = new ExternalGlossary("JohannaTheTrucker.Glossary.AGrowClusterGlossary", "JohannaTheTruckerAGrowClusterGlossary", false, ExternalGlossary.GlossayType.action, GrowClusterSprite ?? throw new Exception("Miossing GrowClusterSprite"));
            AGrowClusterGlossary.AddLocalisation("en", "Grow Cluster", "Adds and or upgrades all of your missile clusters.", null);
            registry.RegisterGlossary(AGrowClusterGlossary);

            ClusterMissile_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusterRocket_Glossary", "JohannaTheTruckerClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, ClusterMissleIcon ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissile_Glossary.AddLocalisation("en", "Cluster Missile", "A swarm of {0} rocklets. On their turn, one flies forward dealing 1 damage on hit. The entire stack is destroyed in one hit.", null);
            registry.RegisterGlossary(ClusterMissile_Glossary);

            ClusterMissileHE_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileHE_Glossary", "JohannaTheTruckerHEClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, HEClusterMissleIcon ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileHE_Glossary.AddLocalisation("en", "Heavy Cluster Missile", "A swarm of {0} heavy rocklets. On their turn, one flies forward dealing 2 damage on hit. The entire stack is destroyed in one hit.", null);
            registry.RegisterGlossary(ClusterMissileHE_Glossary);

            ClusterMissileSeeker_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileSeeker_Glossary", "JohannaTheTruckerSeekerClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, SeekerClusterMissleIcon ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileSeeker_Glossary.AddLocalisation("en", "Seeker Cluster Missile", "A swarm of {0} seeeker rocklets. On their turn, one flies towards the enemy ship dealing 1 damage. The entire stack is destroyed in one hit.", null);
            registry.RegisterGlossary(ClusterMissileSeeker_Glossary);

            ClusterMissileHESeeker_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileHESeeker_Glossary", "JohannaTheTruckerHESeekerClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, HESeekerClusterMissleToken ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileHESeeker_Glossary.AddLocalisation("en", "HE-S Cluster Missile", "A swarm of {0} heavy seeeker rocklets. On their turn, one flies towards the enemy ship dealing 2 damage on hit. The entire stack is destroyed in one hit.", null);
            registry.RegisterGlossary(ClusterMissileHESeeker_Glossary);
        }

        void ICardManifest.LoadManifest(ICardRegistry registry)
        {
            var card_art = ExternalSprite.GetRaw((int)Spr.cards_colorless);

            var hook_art = HookCardSprite ?? throw new Exception();
            var cluster_art = MissileCardSprite ?? throw new Exception();
            var folding_art = FoldingCardSprite ?? throw new Exception();
            var readjust_art = ExternalSprite.GetRaw((int)Spr.cards_Dodge);
            var engine_art = ExternalSprite.GetRaw((int)Spr.cards_ExtraBattery);
            var enemy_shift_art = ExternalSprite.GetRaw((int)Spr.cards_ScootRight);
            var smart_explosives_art = ExternalSprite.GetRaw((int)Spr.cards_Desktop);
            //  var mass_upgrade_art = ExternalSprite.GetRaw((int)Spr.adap);

            ReelInCard = new ExternalCard("JohannaTheTrucker.Cards.ReelIn", typeof(ReelIn), hook_art, JohannaDeck);
            registry.RegisterCard(ReelInCard);
            ReelInCard.AddLocalisation("Reel In");

            ClusterRocketCard = new ExternalCard("JohannaTheTrucker.Cards.ClusterRocket", typeof(ClusterRocket), cluster_art, JohannaDeck);
            registry.RegisterCard(ClusterRocketCard);
            ClusterRocketCard.AddLocalisation("Cluster Rocket");

            SaturationFireCard = new ExternalCard("JohannaTheTrucker.Cards.SaturationFire", typeof(SaturationFire), cluster_art, JohannaDeck);
            registry.RegisterCard(SaturationFireCard);
            SaturationFireCard.AddLocalisation("Saturation Fire");

            ShiftClusterCard = new ExternalCard("JohannaTheTrucker.Cards.ShiftCluster", typeof(ShiftCluster), cluster_art, JohannaDeck);
            registry.RegisterCard(ShiftClusterCard);
            ShiftClusterCard.AddLocalisation("Shift Cluster");

            HEClusterCard = new ExternalCard("JohannaTheTrucker.Cards.HECluster", typeof(HECluster), cluster_art, JohannaDeck);
            registry.RegisterCard(HEClusterCard);
            HEClusterCard.AddLocalisation("HE-Cluster");

            SeekingClusterCard = new ExternalCard("JohannaTheTrucker.Cards.SeekingCluster", typeof(SeekingCluster), cluster_art, JohannaDeck);
            registry.RegisterCard(SeekingClusterCard);
            SeekingClusterCard.AddLocalisation("Seeking Cluster");

            LeapFrogCard = new ExternalCard("JohannaTheTrucker.Cards.LeapFrog", typeof(LeapFrog), hook_art, JohannaDeck);
            registry.RegisterCard(LeapFrogCard);
            LeapFrogCard.AddLocalisation("Leap Frog");

            SmallManeuverCard = new ExternalCard("JohannaTheTrucker.Cards.SmallManeuver", typeof(SmallManeuver), readjust_art, JohannaDeck);
            // registry.RegisterCard(SmallManeuverCard);
            SmallManeuverCard.AddLocalisation("Small Maneuver");

            DoubleHookCard = new ExternalCard("JohannaTheTrucker.Cards.DoubleHook", typeof(DoubleHook), hook_art, JohannaDeck);
            registry.RegisterCard(DoubleHookCard);
            DoubleHookCard.AddLocalisation("Double Hook");

            SpaceFoldingCard = new ExternalCard("JohannaTheTrucker.Cards.SpaceFolding", typeof(SpaceFolding), folding_art, JohannaDeck);
            registry.RegisterCard(SpaceFoldingCard);
            SpaceFoldingCard.AddLocalisation("Space Folding", "Flip midrow using your missile bay as pivot.", "Flip midrow using your missile bay as pivot. Gain 2 midshift.", "Bubble and Flip midrow usinyour missile bay as pivot.");

            VarietyPackCard = new ExternalCard("JohannaTheTrucker.Cards.VarietyPack", typeof(VarietyPack), cluster_art, JohannaDeck);
            registry.RegisterCard(VarietyPackCard);
            VarietyPackCard.AddLocalisation("Variety Pack");

            EnemyShiftCard = new ExternalCard("JohannaTheTrucker.Cards.EnemyShift", typeof(EnemyShift), enemy_shift_art, JohannaDeck);
            registry.RegisterCard(EnemyShiftCard);
            EnemyShiftCard.AddLocalisation("Enemy Shift");

            ReplicatorCard = new ExternalCard("JohannaTheTrucker.Cards.Replicator", typeof(Replicator), cluster_art, JohannaDeck);
            registry.RegisterCard(ReplicatorCard);
            ReplicatorCard.AddLocalisation("Replicator");

            MicroMissilesCard = new ExternalCard("JohannaTheTrucker.Cards.MicroMissiles", typeof(MicroMissiles), cluster_art, JohannaDeck);
            registry.RegisterCard(MicroMissilesCard);
            MicroMissilesCard.AddLocalisation("Micro Missiles");

            ReadjustCard = new ExternalCard("JohannaTheTrucker.Cards.Readjust", typeof(Readjust), readjust_art, JohannaDeck);
            registry.RegisterCard(ReadjustCard);
            ReadjustCard.AddLocalisation("Readjust");

            RocketSiloCard = new ExternalCard("JohannaTheTrucker.Cards.RocketSilo", typeof(RocketSilo), cluster_art, JohannaDeck);
            registry.RegisterCard(RocketSiloCard);
            RocketSiloCard.AddLocalisation("Rocket Silo");

            SmartExplosivesCard = new ExternalCard("JohannaTheTrucker.Cards.SmartExplosives", typeof(SmartExplosives), smart_explosives_art, JohannaDeck);
            registry.RegisterCard(SmartExplosivesCard);
            SmartExplosivesCard.AddLocalisation("Smart Explosives");

            MultiplicityCard = new ExternalCard("JohannaTheTrucker.Cards.Multiplicity", typeof(Multiplicity), cluster_art, JohannaDeck);
            registry.RegisterCard(MultiplicityCard);
            MultiplicityCard.AddLocalisation("Multiplicity");

            MassUpgradeCard = new ExternalCard("JohannaTheTrucker.Cards.MassUpgrade", typeof(MassUpgrade), cluster_art, JohannaDeck);
            registry.RegisterCard(MassUpgradeCard);
            MassUpgradeCard.AddLocalisation("Mass Upgrade");

            ReboundCard = new ExternalCard("JohannaTheTrucker.Cards.Rebound", typeof(Rebound), hook_art, JohannaDeck);
            registry.RegisterCard(ReboundCard);
            ReboundCard.AddLocalisation("Rebound");

            BigSwingCard = new ExternalCard("JohannaTheTrucker.Cards.BigSwing", typeof(BigSwing), hook_art, JohannaDeck);
            registry.RegisterCard(BigSwingCard);
            BigSwingCard.AddLocalisation("Big Swing", "Hook <c=keyword>{0}</c> but move twice the distance (<c=keyword>{1}</c>)");

            FireFireFireCard = new ExternalCard("JohannaTheTrucker.Cards.FireFireFire", typeof(FireFireFire), cluster_art, JohannaDeck);
            registry.RegisterCard(FireFireFireCard);
            FireFireFireCard.AddLocalisation("Fire! Fire! Fire!", "All missiles shoot until fully spent or unable to shoot.", null, "All missiles fire once.");

            EngineStallCard = new ExternalCard("JohannaTheTrucker.Cards.EngineStall", typeof(EngineStall), engine_art, JohannaDeck);
            registry.RegisterCard(EngineStallCard);
            EngineStallCard.AddLocalisation("Engine Stall");

            BasicDefensivesCard = new ExternalCard("JohannaTheTrucker.Cards.BasicDefensives", typeof(BasicDefensives), card_art);
            registry.RegisterCard(BasicDefensivesCard);
            BasicDefensivesCard.AddLocalisation("Basic Defensives");

            BasicFileSearchCard = new ExternalCard("JohannaTheTrucker.Cards.BasicFileSearch", typeof(BasicFileSearch), card_art);
            registry.RegisterCard(BasicFileSearchCard);
            BasicFileSearchCard.AddLocalisation("Basic File Search");
        }

        void ICharacterManifest.LoadManifest(ICharacterRegistry registry)
        {
            JohannaCharacter = new ExternalCharacter("JohannaTheTrucker.Character.Johanna",
                JohannaDeck ?? throw new Exception("Missing Deck"),
                JohannaPanelFrame ?? throw new Exception("Missing Potrait"),
                new Type[] { typeof(ReelIn), typeof(ClusterRocket) },
                new Type[0],
                JohannaDefaultAnimation ?? throw new Exception("missing default animation"),
                JohannaMiniAnimation ?? throw new Exception("missing mini animation"));

            JohannaCharacter.AddNameLocalisation("Johanna");

            JohannaCharacter.AddDescLocalisation("<c=dizzy>JOHANNA</c>\nA private transporter. Her cards launch and combine a variety of missiles and uses them to reposition the ship.");

            registry.RegisterCharacter(JohannaCharacter);
        }

        void IAnimationManifest.LoadManifest(IAnimationRegistry registry)
        {
            JohannaDefaultAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.JohannaDefault",
                JohannaDeck ?? throw new Exception("missing deck"),
                "neutral", false,
                new ExternalSprite[] { JohannaPotrait ?? throw new Exception("missing potrait") });

            registry.RegisterAnimation(JohannaDefaultAnimation);

            JohannaMiniAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.JohannaMini",
               JohannaDeck ?? throw new Exception("missing deck"),
               "mini", false,
               new ExternalSprite[] { JohannaMini ?? throw new Exception("missing mini") });

            registry.RegisterAnimation(JohannaMiniAnimation);

            TalkAngryAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkAngry", JohannaDeck, "talk_angry", false, TalkAngrySprites);
            registry.RegisterAnimation(TalkAngryAnimation);

            TalkLaughAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkLaugh", JohannaDeck, "talk_laugh", false, TalkLaughSprites);
            registry.RegisterAnimation(TalkLaughAnimation);

            TalkNeutralAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkNeutral", JohannaDeck, "talk_neutral", false, TalkNeutralSprites);
            registry.RegisterAnimation(TalkNeutralAnimation);

            TalkReminisceAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkReminisce", JohannaDeck, "talk_reminisce", false, TalkReminisceSprites);
            registry.RegisterAnimation(TalkReminisceAnimation);

            TalkSadAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkSad", JohannaDeck, "talk_sad", false, TalkSadSprites);
            registry.RegisterAnimation(TalkSadAnimation);

            TalkScaredAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.TalkScared", JohannaDeck, "talk_scared", false, TalkScaredSprites);
            registry.RegisterAnimation(TalkScaredAnimation);

            GameoverAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.GameOver", JohannaDeck, "gameover", false, TalkScaredSprites);
            registry.RegisterAnimation(GameoverAnimation);
        }

        public void BootMod(IModLoaderContact contact)
        {
            {
                //create action draw code for agrow cluster
                var harmony = new Harmony("EWanderer.JohannaTheTrucker.AGrowClusterRendering");

                var card_render_action_method = typeof(Card).GetMethod("RenderAction", BindingFlags.Public | BindingFlags.Static) ?? throw new Exception();

                var card_render_action_prefix = this.GetType().GetMethod("AGrowClusterRenderActionPrefix", BindingFlags.NonPublic | BindingFlags.Static);

                harmony.Patch(card_render_action_method, prefix: new HarmonyMethod(card_render_action_prefix));

            }
        }


        private static bool AGrowClusterRenderActionPrefix(Card __instance, ref int __result, G g, State state, CardAction action, bool dontDraw = false, int shardAvailable = 0, int stunChargeAvailable = 0, int bubbleJuiceAvailable = 0)
        {
            if (action is not AGrowClusters grow_cluster_action)
            {
                return true;
            }

            Color spriteColor = action.disabled ? Colors.disabledIconTint : new Color("ffffff");
            __result = 0;
            if (Manifest.GrowClusterSprite?.Id == null)
                return false;

            int? cluster_spr_val = null;
            switch (grow_cluster_action.cluster_type)
            {
                case MidrowStuff.ClusterMissile.MissileType.normal:
                    cluster_spr_val = Manifest.ClusterMissleIcon?.Id;
                    break;
                case MidrowStuff.ClusterMissile.MissileType.heavy:
                    cluster_spr_val = Manifest.HEClusterMissleIcon?.Id;
                    break;
                case MidrowStuff.ClusterMissile.MissileType.seeker:
                    cluster_spr_val = Manifest.SeekerClusterMissleIcon?.Id;
                    break;
                case MidrowStuff.ClusterMissile.MissileType.heavy_seeker:
                    cluster_spr_val = Manifest.HESeekerClusterMissleToken?.Id;
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (cluster_spr_val == null)
                return false;

            Icon multiplicity_icon = new Icon((Spr)Manifest.GrowClusterSprite.Id, null, Colors.textMain, false);
            Icon cluster_icon = new Icon((Spr)cluster_spr_val, null, Colors.textMain, false);
            int w = 0;
            int iconWidth = 8;
            int iconNumberPadding = 2;
            int numberWidth = 6;
            bool isFirst = true;
            IconAndOrNumber(multiplicity_icon.path, null, new Color?(action.disabled ? Colors.disabledText : multiplicity_icon.color), false, null);
            IconAndOrNumber(cluster_icon.path, grow_cluster_action.ammount, new Color?(action.disabled ? Colors.disabledText : cluster_icon.color), false, null);




            __result = w;
            return false;

            void IconAndOrNumber(Spr icon, int? amount = null, Color? textColor = null, bool flipY = false, int? x = null)
            {
                if (!isFirst)
                    w += 4;
                Rect? nullable1;
                if (!dontDraw)
                {
                    nullable1 = new Rect?(new Rect((double)w));
                    global::UIKey? key = new global::UIKey?();
                    Rect? rect = nullable1;
                    Rect? rectForReticle = new Rect?();
                    global::UIKey? rightHint = new global::UIKey?();
                    global::UIKey? leftHint = new global::UIKey?();
                    global::UIKey? upHint = new global::UIKey?();
                    global::UIKey? downHint = new global::UIKey?();
                    Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
                    Spr? id = new Spr?(icon);
                    double x1 = xy.x;
                    double y = xy.y;
                    int num = flipY ? 1 : 0;
                    Color? nullable2 = new Color?(spriteColor);
                    Vec? originPx = new Vec?();
                    Vec? originRel = new Vec?();
                    Vec? scale = new Vec?();
                    nullable1 = new Rect?();
                    Rect? pixelRect = nullable1;
                    Color? color = nullable2;
                    Draw.Sprite(id, x1, y, flipY: num != 0, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
                    g.Pop();
                }
                w += iconWidth;
                Color? nullable3;
                if (amount.HasValue)
                {
                    int valueOrDefault = amount.GetValueOrDefault();
                    if (!x.HasValue)
                    {
                        w += iconNumberPadding;
                        string str = DB.IntStringCache(valueOrDefault);
                        if (!dontDraw)
                        {
                            nullable1 = new Rect?(new Rect((double)w));
                            global::UIKey? key = new global::UIKey?();
                            Rect? rect = nullable1;
                            Rect? rectForReticle = new Rect?();
                            global::UIKey? rightHint = new global::UIKey?();
                            global::UIKey? leftHint = new global::UIKey?();
                            global::UIKey? upHint = new global::UIKey?();
                            global::UIKey? downHint = new global::UIKey?();
                            Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
                            int number = valueOrDefault;
                            double x2 = xy.x;
                            double y = xy.y;
                            nullable3 = textColor;
                            Color color = nullable3 ?? Colors.textMain;
                            BigNumbers.Render(number, x2, y, color);
                            g.Pop();
                        }
                        w += str.Length * numberWidth;
                    }
                }
                if (x.HasValue)
                {
                    int? nullable4 = x;
                    int num = 0;
                    if (nullable4.GetValueOrDefault() < num & nullable4.HasValue)
                    {
                        w += iconNumberPadding;
                        if (!dontDraw)
                        {
                            nullable1 = new Rect?(new Rect((double)(w - 2)));
                            global::UIKey? key = new global::UIKey?();
                            Rect? rect = nullable1;
                            Rect? rectForReticle = new Rect?();
                            global::UIKey? rightHint = new global::UIKey?();
                            global::UIKey? leftHint = new global::UIKey?();
                            global::UIKey? upHint = new global::UIKey?();
                            global::UIKey? downHint = new global::UIKey?();
                            Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
                            Spr? id = new Spr?(Spr.icons_minus);
                            double x3 = xy.x;
                            double y = xy.y - 1.0;
                            nullable3 = action.disabled ? new Color?(spriteColor) : textColor;
                            Vec? originPx = new Vec?();
                            Vec? originRel = new Vec?();
                            Vec? scale = new Vec?();
                            nullable1 = new Rect?();
                            Rect? pixelRect = nullable1;
                            Color? color = nullable3;
                            Draw.Sprite(id, x3, y, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
                            g.Pop();
                        }
                        w += 3;
                    }
                    if (Math.Abs(x.Value) > 1)
                    {
                        w += iconNumberPadding + 1;
                        if (!dontDraw)
                        {
                            nullable1 = new Rect?(new Rect((double)w));
                            global::UIKey? key = new global::UIKey?();
                            Rect? rect = nullable1;
                            Rect? rectForReticle = new Rect?();
                            global::UIKey? rightHint = new global::UIKey?();
                            global::UIKey? leftHint = new global::UIKey?();
                            global::UIKey? upHint = new global::UIKey?();
                            global::UIKey? downHint = new global::UIKey?();
                            Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
                            int number = Math.Abs(x.Value);
                            double x4 = xy.x;
                            double y = xy.y;
                            nullable3 = textColor;
                            Color color = nullable3 ?? Colors.textMain;
                            BigNumbers.Render(number, x4, y, color);
                            g.Pop();
                        }
                        w += 4;
                    }
                    w += iconNumberPadding;
                    if (!dontDraw)
                    {

                        nullable1 = new Rect?(new Rect((double)w));
                        global::UIKey? key = new global::UIKey?();
                        Rect? rect = nullable1;
                        Rect? rectForReticle = new Rect?();
                        global::UIKey? rightHint = new global::UIKey?();
                        global::UIKey? leftHint = new global::UIKey?();
                        global::UIKey? upHint = new global::UIKey?();
                        global::UIKey? downHint = new global::UIKey?();
                        Vec xy = g.Push(key, rect, rectForReticle, rightHint: rightHint, leftHint: leftHint, upHint: upHint, downHint: downHint).rect.xy;
                        Spr? id = new Spr?(Spr.icons_x_white);
                        double x5 = xy.x;
                        double y = xy.y - 1.0;
                        Icon? icon1 = action.GetIcon(state);
                        ref Icon? local = ref icon1;
                        nullable3 = local.HasValue ? new Color?(local.GetValueOrDefault().color) : new Color?();
                        Vec? originPx = new Vec?();
                        Vec? originRel = new Vec?();
                        Vec? scale = new Vec?();
                        nullable1 = new Rect?();
                        Rect? pixelRect = nullable1;
                        Color? color = nullable3;
                        Draw.Sprite(id, x5, y, originPx: originPx, originRel: originRel, scale: scale, pixelRect: pixelRect, color: color);
                        g.Pop();
                    }
                    w += 8;
                }
                isFirst = false;
            }
        }

    }
}
