using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using JohannaTheTrucker.MidrowStuff;

namespace JohannaTheTrucker
{
    public class ArtifactLogicManifest : ICustomEventManifest, IArtifactManifest, ISpriteManifest
    {
        public DirectoryInfo? ModRootFolder { get; set; }

        public string Name => "Actionmartini.JohannaTheTrucker.Artifacts";

        public Assembly CobaltCoreAssembly => throw new NotImplementedException();

        public IEnumerable<string> Dependencies => throw new NotImplementedException();

        private static ICustomEventHub? _eventHub;

        internal static ICustomEventHub EventHub { get => _eventHub ?? throw new Exception(); set => _eventHub = value; }

        public void LoadManifest(ICustomEventHub eventHub)
        {
            //assign for local consumption
            _eventHub= eventHub;
            eventHub.MakeEvent<Tuple<ClusterMissile, Combat, State>>("JohannaTheTrucker.ClusterMissileExpended");
        }

        public void LoadManifest(IArtifactRegistry registry)
        {

        }

        public void LoadManifest(IArtRegistry artRegistry)
        {

        }


    }

}
