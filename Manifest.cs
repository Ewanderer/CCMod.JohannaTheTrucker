using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using JohannaTheTrucker.Cards;

namespace JohannaTheTrucker
{
    public partial class Manifest : ISpriteManifest, IDeckManifest, IGlossaryManifest, ICardManifest, ICharacterManifest, IAnimationManifest, IStatusManifest
    {
        public static ExternalGlossary? AHook_Glossary { get; private set; }
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
        public static ExternalCharacter? JohannaCharacter { get; private set; }
        public static ExternalDeck? JohannaDeck { get; private set; }
        public static ExternalAnimation? JohannaDefaultAnimation { get; private set; }
        public static ExternalSprite? JohannaMini { get; private set; }
        public static ExternalAnimation? JohannaMiniAnimation { get; private set; }
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
        public static ExternalCard? ReadjustCard { get; private set; }
        public static ExternalCard? RocketSiloCard { get; private set; }
        public static ExternalCard? SmartExplosivesCard { get; private set; }
        public static ExternalSprite? SeekerClusterMissleIcon { get; private set; }
        public static ExternalSprite? SeekerClusterMissleToken { get; private set; }
        IEnumerable<string> ISpriteManifest.Dependencies => new string[0];

        public DirectoryInfo? ModRootFolder { get; set; }
        public string Name { get; init; } = "Actionmartini.JohannaTheTrucker";

        void ISpriteManifest.LoadManifest(IArtRegistry artRegistry)
        {
            if (ModRootFolder == null)
                throw new Exception("Root Folder not set");
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


        }

        void ICardManifest.LoadManifest(ICardRegistry registry)
        {
            var card_art = ExternalSprite.GetRaw((int)Spr.cards_colorless);
            ReelInCard = new ExternalCard("JohannaTheTrucker.Cards.ReelIn", typeof(ReelIn), card_art, JohannaDeck);
            registry.RegisterCard(ReelInCard);
            ReelInCard.AddLocalisation("Reel In");

            ClusterRocketCard = new ExternalCard("JohannaTheTrucker.Cards.ClusterRocket", typeof(ClusterRocket), card_art, JohannaDeck);
            registry.RegisterCard(ClusterRocketCard);
            ClusterRocketCard.AddLocalisation("Cluster Rocket");

            SaturationFireCard = new ExternalCard("JohannaTheTrucker.Cards.SaturationFire", typeof(SaturationFire), card_art, JohannaDeck);
            registry.RegisterCard(SaturationFireCard);
            SaturationFireCard.AddLocalisation("Saturation Fire");

            LargePayloadCard = new ExternalCard("JohannaTheTrucker.Cards.LargePayload", typeof(LargePayload), card_art, JohannaDeck);
            registry.RegisterCard(LargePayloadCard);
            LargePayloadCard.AddLocalisation("Large Payload");

            HEClusterCard = new ExternalCard("JohannaTheTrucker.Cards.HECluster", typeof(HECluster), card_art, JohannaDeck);
            registry.RegisterCard(HEClusterCard);
            HEClusterCard.AddLocalisation("HE-Cluster");

            SeekingClusterCard = new ExternalCard("JohannaTheTrucker.Cards.SeekingCluster", typeof(SeekingCluster), card_art, JohannaDeck);
            registry.RegisterCard(SeekingClusterCard);
            SeekingClusterCard.AddLocalisation("Seeking Cluster");

            LeapFrogCard = new ExternalCard("JohannaTheTrucker.Cards.LeapFrog", typeof(LeapFrog), card_art, JohannaDeck);
            registry.RegisterCard(LeapFrogCard);
            LeapFrogCard.AddLocalisation("Leap Frog");

            SmallManeuverCard = new ExternalCard("JohannaTheTrucker.Cards.SmallManeuver", typeof(SmallManeuver), card_art, JohannaDeck);
            registry.RegisterCard(SmallManeuverCard);
            SmallManeuverCard.AddLocalisation("Small Maneuver");

            DoubleHookCard = new ExternalCard("JohannaTheTrucker.Cards.DoubleHook", typeof(DoubleHook), card_art, JohannaDeck);
            registry.RegisterCard(DoubleHookCard);
            DoubleHookCard.AddLocalisation("Double Hook");

            SpaceFoldingCard = new ExternalCard("JohannaTheTrucker.Cards.SpaceFolding", typeof(SpaceFolding), card_art, JohannaDeck);
            registry.RegisterCard(SpaceFoldingCard);
            SpaceFoldingCard.AddLocalisation("Space Folding", "Flip midrow using your missile bay as pivot.", "Flip midrow using your missile bay as pivot. Gain 2 midshift.", "Bubble and Flip midrow using your missile bay as pivot.");

            VarietyPackCard = new ExternalCard("JohannaTheTrucker.Cards.VarietyPack", typeof(VarietyPack), card_art, JohannaDeck);
            registry.RegisterCard(VarietyPackCard);
            VarietyPackCard.AddLocalisation("Variety Pack");

            EnemyShiftCard = new ExternalCard("JohannaTheTrucker.Cards.EnemyShift", typeof(EnemyShift), card_art, JohannaDeck);
            registry.RegisterCard(EnemyShiftCard);
            EnemyShiftCard.AddLocalisation("Enemy Shift");

            ReplicatorCard = new ExternalCard("JohannaTheTrucker.Cards.Replicator", typeof(Replicator), card_art, JohannaDeck);
            registry.RegisterCard(ReplicatorCard);
            ReplicatorCard.AddLocalisation("Replicator");

            MicroMissilesCard = new ExternalCard("JohannaTheTrucker.Cards.MicroMissiles", typeof(MicroMissiles), card_art, JohannaDeck);
            registry.RegisterCard(MicroMissilesCard);
            MicroMissilesCard.AddLocalisation("Micro Missiles");

            ReadjustCard = new ExternalCard("JohannaTheTrucker.Cards.Readjust", typeof(Readjust), card_art, JohannaDeck);
            registry.RegisterCard(ReadjustCard);
            ReadjustCard.AddLocalisation("Readjust");

            RocketSiloCard = new ExternalCard("JohannaTheTrucker.Cards.RocketSilo", typeof(RocketSilo), card_art, JohannaDeck);
            registry.RegisterCard(RocketSiloCard);
            RocketSiloCard.AddLocalisation("Rocket Silo");

            SmartExplosivesCard = new ExternalCard("JohannaTheTrucker.Cards.SmartExplosives", typeof(SmartExplosives), card_art, JohannaDeck);
            registry.RegisterCard(SmartExplosivesCard);
            SmartExplosivesCard.AddLocalisation("Smart Explosives");
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
        }


    }
}