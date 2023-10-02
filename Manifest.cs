using CobaltCoreModding.Definitions.ModManifests;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using System.Reflection;
using JohannaTheTrucker.Cards;

namespace JohannaTheTrucker
{
    public class Manifest : ISpriteManifest, IDeckManifest, IGlossaryManifest, ICardManifest, ICharacterManifest, IAnimationManifest
    {
        IEnumerable<string> ISpriteManifest.Dependencies => new string[0];

        public string Name { get; init; } = "Arin.JohannaTheTrucker";

       public static ExternalSprite? JohannaPotrait { get; private set; }
       public static ExternalSprite? JohannaMini { get; private set; }
       public static ExternalSprite? HookIcon { get; private set; }
       public static ExternalSprite? ClusterMissleIcon { get; private set; }
       public static ExternalDeck? JohannaDeck { get; private set; }
        public static ExternalCard? ReelInCard { get; private set; }
        public static ExternalCard? ClusterRocketCard { get; private set; }
        public static ExternalCharacter? JohannaCharacter { get; private set; }
        public static ExternalAnimation? JohannaDefaultAnimation { get; private set; }
        public static ExternalAnimation? JohannaMiniAnimation { get; private set; }
        public DirectoryInfo? ModRootFolder { get; set; }

        void ISpriteManifest.LoadManifest(IArtRegistry artRegistry)
        {
            if (ModRootFolder == null)
                throw new Exception("Root Folder not set");
            //load the character sprite

            {
                var path = Path.Combine(ModRootFolder.FullName,"Sprites",Path.GetFileName("JohannaDefault.png"));
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

            //cluster missile sprite

            {
                var path = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ClusterMissile.png"));
                ClusterMissleIcon = new ExternalSprite("JohannaTheTrucker.ClusterMissleIcon", new FileInfo(path));
                artRegistry.RegisterArt(ClusterMissleIcon);
            }
        }

        public void LoadManifest(IDeckRegistry registry)
        {
            ExternalSprite cardArtDefault = ExternalSprite.GetRaw((int)Spr.cards_colorless);
            ExternalSprite borderSprite = ExternalSprite.GetRaw((int)Spr.cardShared_border_jupiter);
            JohannaDeck = new ExternalDeck(
                "JohannaTheTrucker.JohannaDeck",
                System.Drawing.Color.FromArgb(65, 144, 195),
                System.Drawing.Color.FromArgb(195, 41, 108),
                cardArtDefault, 
                borderSprite, 
                null);
            registry.RegisterDeck(JohannaDeck);
        }

        void IGlossaryManifest.LoadManifest(IGlossaryRegisty registry)
        {
            {
                var glossary = new ExternalGlossary("JohannaTheTrucker.Glossary.AHookDesc", "JohannaTheTruckerAHook", false, ExternalGlossary.GlossayType.action, HookIcon??throw new Exception("Miossing Hook Icon"));
                glossary.AddLocalisation("en", "Hookshot", "Align ship's missile bay with the closest midrow object", null);
                registry.RegisterGlossary(glossary);
            }

        }

        void ICardManifest.LoadManifest(ICardRegistry registry)
        {
            var card_art = ExternalSprite.GetRaw((int)Spr.cards_colorless);
            ReelInCard = new ExternalCard("JohannaTheTrucker.Cards.ReelIn", typeof(ReelIn),card_art, JohannaDeck);
            registry.RegisterCard(ReelInCard);

            ReelInCard.AddLocalisation("Reel In");

            ClusterRocketCard = new ExternalCard("JohannaTheTrucker.Cards.ClusterRocket", typeof(ClusterRocket), card_art, JohannaDeck);
            registry.RegisterCard(ClusterRocketCard);
            ClusterRocketCard.AddLocalisation("Cluster Rocket");

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
                new ExternalSprite[] { JohannaPotrait??throw new Exception("missing potrait") });

            registry.RegisterAnimation(JohannaDefaultAnimation);

            JohannaMiniAnimation = new ExternalAnimation("JohannaTheTrucker.Animation.JohannaMini",
               JohannaDeck ?? throw new Exception("missing deck"),
               "mini", false,
               new ExternalSprite[] { JohannaMini ?? throw new Exception("missing mini") });

            registry.RegisterAnimation(JohannaMiniAnimation);
        }
    }
}