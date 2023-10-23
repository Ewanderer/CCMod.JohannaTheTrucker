using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using JohannaTheTrucker.Artifacts;
using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker
{
    public partial class Manifest
    {


        private static ICustomEventHub? _eventHub;

        internal static ICustomEventHub EventHub { get => _eventHub ?? throw new Exception(); set => _eventHub = value; }

        public static ExternalArtifact? DecorativeSalmonArtifact { get; private set; }

        public void LoadManifest(ICustomEventHub eventHub)
        {
            //assign for local consumption
            _eventHub = eventHub;
            eventHub.MakeEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended");
        }

        public void LoadManifest(IArtifactRegistry registry)
        {
            DecorativeSalmonArtifact = new ExternalArtifact(typeof(DecorativeSalmon), "JohannaTheTrucker.Artifacts.DecorativeSalmon", DecorativeSalmonSprite ?? throw new Exception("missing deco salom sprite"), Manifest.JohannaDeck ?? throw new Exception("missing johanna deck."), new ExternalGlossary[0]);

            DecorativeSalmonArtifact.AddLocalisation("en", "Decorative Salmon", "Decorative Salmon: Every time a cluster is completely spent (not destroyed) gain 1 Shield");

            registry.RegisterArtifact(DecorativeSalmonArtifact);
        }


    }

}
