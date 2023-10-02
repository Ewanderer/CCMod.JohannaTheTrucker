using CobaltCoreModding.Definitions.ModManifests;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using System.Reflection;

namespace JohannaTheTrucker
{
    public class Manifest : ISpriteManifest, IDeckManifest
    {
        IEnumerable<string> ISpriteManifest.Dependencies => new string[0];

        public string Name { get; init; } = "Arin.JohannaTheTrucker";

       public ExternalSprite? JohannaPotrait { get; private set; }
       public ExternalSprite? JohannaMini { get; private set; }
       public ExternalSprite? HookIcon { get; private set; }
       public ExternalSprite? ClusterMissleIcon { get; private set; }
       public ExternalDeck? JohannaDeck { get; private set; }

        public DirectoryInfo? ModRootFolder { get; set; }

        void ISpriteManifest.LoadManifest(IArtRegistry artRegistry)
        {
            if (ModRootFolder == null)
                throw new Exception("Root Folder not set");
            //load the character sprite

            {
                var path = Path.Combine(ModRootFolder.FullName,"Sprites",Path.GetFileName("JohannaCropped.png"));
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
            ExternalSprite cardArtDefault = null;
            ExternalSprite borderSprite = null;
            JohannaDeck = new ExternalDeck(
                "JohannaTheTrucker.JohannaDeck",
                System.Drawing.Color.FromArgb(65, 144, 195),
                System.Drawing.Color.FromArgb(195, 41, 10), cardArtDefault, borderSprite, null);
            registry.RegisterDeck(JohannaDeck);
        }
    }
}