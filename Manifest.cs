using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using JohannaTheTrucker.Cards;

namespace JohannaTheTrucker
{
    public partial class Manifest : ISpriteManifest, IDeckManifest, IGlossaryManifest, ICardManifest, ICharacterManifest, IAnimationManifest, IStatusManifest, ICustomEventManifest, IArtifactManifest
    {
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
        public static ExternalSprite? DecorativeSalmonSprite { get; private set; }
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
        public static ExternalCard? LargePayloadCard { get; private set; }
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
        public static ExternalSprite? SeekerClusterMissleIcon { get; private set; }
        public static ExternalSprite? SeekerClusterMissleToken { get; private set; }
        public static ExternalSprite? FoldingCardSprite { get; private set; }
        public static ExternalSprite? HookCardSprite { get; private set; }
        public static ExternalSprite? MissileCardSprite { get; private set; }
        public static ExternalSprite? StallCardSprite { get; private set; }

        public static ExternalGlossary? AGrowClusterGlossary { get; private set; }
        IEnumerable<string> ISpriteManifest.Dependencies => new string[0];

        public static List<ExternalSprite> TalkAngrySprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkLaughSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkNeutralSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkReminisceSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkSadSprites { get; private set; } = new List<ExternalSprite>();
        public static List<ExternalSprite> TalkScaredSprites { get; private set; } = new List<ExternalSprite>();

        public DirectoryInfo? ModRootFolder { get; set; }
        public string Name { get; init; } = "Actionmartini.JohannaTheTrucker";

        void ISpriteManifest.LoadManifest(IArtRegistry artRegistry)
        {
            if (ModRootFolder == null)
                throw new Exception("Root Folder not set");
            //artifacts

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites","artifact_icons", Path.GetFileName("decorative_salmon.png"));
                DecorativeSalmonSprite = new ExternalSprite("JohannaTheTrucker.decorative_salmon", new FileInfo(path));
                artRegistry.RegisterArt(DecorativeSalmonSprite);
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

            //talk animations
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
            ClusterMissile_Glossary.AddLocalisation("en", "Cluster Missile", "A swarm of {0} rocklets. On their turn 1 flies forward dealing 1 damage on a hit. The entire stack dies in one hit.", null);
            registry.RegisterGlossary(ClusterMissile_Glossary);

            ClusterMissileHE_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileHE_Glossary", "JohannaTheTruckerHEClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, HEClusterMissleIcon ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileHE_Glossary.AddLocalisation("en", "Heavy Cluster Missile", "A swarm of {0} heavy rocklets. On their turn 1 flies forward dealing 2 damage on a hit. The entire stack dies in one hit.", null);
            registry.RegisterGlossary(ClusterMissileHE_Glossary);

            ClusterMissileSeeker_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileSeeker_Glossary", "JohannaTheTruckerSeekerClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, SeekerClusterMissleIcon ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileSeeker_Glossary.AddLocalisation("en", "Seeker Cluster Missile", "A swarm of {0} seeeker rocklets. On their turn 1 flies towards their target dealing 1 damage. The entire stack dies in one hit.", null);
            registry.RegisterGlossary(ClusterMissileSeeker_Glossary);

            ClusterMissileHESeeker_Glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.ClusteMissileHESeeker_Glossary", "JohannaTheTruckerHESeekerClusterRocketMidRow", false, ExternalGlossary.GlossayType.midrow, HESeekerClusterMissleToken ?? throw new Exception("Missing ClusterMissleIcon"));
            ClusterMissileHESeeker_Glossary.AddLocalisation("en", "HE-S Cluster Missile", "A swarm of {0} heavy seeeker rocklets. On their turn 2 flies towards their target dealing 1 damage. The entire stack dies in one hit.", null);
            registry.RegisterGlossary(ClusterMissileHESeeker_Glossary);
        }

        void ICardManifest.LoadManifest(ICardRegistry registry)
        {
            var card_art = ExternalSprite.GetRaw((int)Spr.cards_colorless);

            var hook_art = HookCardSprite ?? throw new Exception();
            var cluster_art = MissileCardSprite ?? throw new Exception();
            var folding_art = FoldingCardSprite ?? throw new Exception();
            var readjust_art = ExternalSprite.GetRaw((int)Spr.cards_Dodge);
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

            LargePayloadCard = new ExternalCard("JohannaTheTrucker.Cards.LargePayload", typeof(LargePayload), cluster_art, JohannaDeck);
            registry.RegisterCard(LargePayloadCard);
            LargePayloadCard.AddLocalisation("Large Payload");

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
            registry.RegisterCard(SmallManeuverCard);
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
            BigSwingCard.AddLocalisation("Big Swing", "Hook <c=keyword>{0}</c> but move thrice the distance (<c=keyword>{1}</c>)");

            FireFireFireCard = new ExternalCard("JohannaTheTrucker.Cards.FireFireFire", typeof(FireFireFire), cluster_art, JohannaDeck);
            registry.RegisterCard(FireFireFireCard);
            FireFireFireCard.AddLocalisation("Fire! Fire! Fire!", "All missiles shoot until fully spent or unable to shoot.", null, "All missiles fire once.");
        }

        void ICharacterManifest.LoadManifest(ICharacterRegistry registry)
        {
            JohannaCharacter = new ExternalCharacter("JohannaTheTrucker.Character.Johanna",
                JohannaDeck ?? throw new Exception("Missing Deck"),
                JohannaPotrait ?? throw new Exception("Missing Potrait"),
                new Type[] { typeof(ReelIn), typeof(ClusterRocket) },
                new Type[0],
                JohannaDefaultAnimation ?? throw new Exception("missing default animation"),
                JohannaMiniAnimation ?? throw new Exception("missing mini animation"));

            JohannaCharacter.AddNameLocalisation("Johanna");

            JohannaCharacter.AddDescLocalisation("A space trucker. Uses a grappling hook and cluster missiles to deal with pirates.");

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


    }
}