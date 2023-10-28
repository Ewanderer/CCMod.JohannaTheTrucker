using JohannaTheTrucker.MidrowStuff;
using JohannaTheTrucker.SpecialEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Actions
{
    public class AGrowClusters : CardAction
    {
        public ClusterMissile.MissileType cluster_type;
        public int ammount;
        public bool bubble;

        public bool fromPlayer;

        public override void Begin(G g, State s, Combat c)
        {

            var start_x = fromPlayer ? s.ship.x : c.otherShip.x;
            var start_y = fromPlayer ? FlightFX.YRow.player : FlightFX.YRow.enemy;
            foreach (var stuff in c.stuff.Values)
            {
                if (stuff.fromPlayer != fromPlayer)
                    continue;
                if (stuff is not ClusterMissile cluster)
                    continue;
                cluster.stackSize += ammount;

                if (cluster_type != ClusterMissile.MissileType.normal && cluster.stackType != cluster_type)
                {
                    //check if the upgrade is just to the other type
                    if (cluster.stackType == ClusterMissile.MissileType.normal)
                        cluster.stackType = cluster_type;
                    //of if it becomes the ultimate version.
                    else cluster.stackType = ClusterMissile.MissileType.heavy_seeker;
                }

                if (bubble)
                    cluster.bubbleShield = true;
                if (ammount > 0)
                {
                    c.fx.Add(new FlightFX()
                    {
                        start_x = start_x,
                        start_y = start_y,
                        target_x = cluster.x,
                        target_y = FlightFX.YRow.midrow,
                        target_flight_time = 0.6,
                        miss = false,
                        texture = SpriteLoader.Get(cluster.GetIcon() ?? Spr.icons_recycle)
                    });
                }
                // else
                {
                    c.fx.Add(new GlowFX()
                    {
                        color = Colors.healing,
                        pos = FxPositions.Drone(cluster.x),
                        size = new Vec(24, 24),
                        age = -0.8
                    });
                }
                if (ammount > 0)
                    Manifest.EventHub.SignalEvent<Tuple<ClusterMissile,State>>("JohannaTheTrucker.ClusterMissileGrown", new(cluster,s));
            }
            timer = 0.8;
        }



        public override Icon? GetIcon(State s)
        {
            if (Manifest.GrowClusterSprite?.Id == null)
                return null;
            return new Icon((Spr)Manifest.GrowClusterSprite.Id, null, Colors.textMain, false);
        }

        public override List<Tooltip> GetTooltips(State s)
        {
            var demo_missile = new ClusterMissile() { stackType = this.cluster_type };
            var missle_tt = demo_missile.GetTooltips();
            var result = new List<Tooltip>() {
                new TTGlossary(Manifest.AGrowClusterGlossary?.Head??throw new Exception()),
            };
            result.AddRange(missle_tt);
            if (bubble)
                result.Add(new TTGlossary("midrow.bubbleShield"));
            return result;
        }

    }
}
