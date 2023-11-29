using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using FMOD;
using FSPRO;
using HarmonyLib;
using JohannaTheTrucker.MidrowStuff;
using JohannaTheTrucker.SpecialEffects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker
{

    /// <summary>
    /// Contains the code for the manifest
    /// </summary>
    public class ClusterLaunchManifest : IModManifest
    {

        DirectoryInfo? IManifest.ModRootFolder { get; set; }

        string IManifest.Name => "Actionmartini.JohannaTheTrucker.ClusterLaunch";

        public DirectoryInfo? GameRootFolder { get; set; }

        public IEnumerable<DependencyEntry> Dependencies => Array.Empty<DependencyEntry>();

        public ILogger? Logger { get; set; }

        void IModManifest.BootMod(IModLoaderContact contact)
        {
            Harmony harmony = new Harmony("JohannaTheTrucker.ClusterLaunch");

            var begin_method = typeof(ASpawn).GetMethod("Begin") ?? throw new Exception("ASpawn.Begin method not found.");

            var begin_prefix = typeof(ClusterLaunchManifest).GetMethod("ASpawn_Begin_Prefix", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("ClusterLaunchManifest.ASpawn_Begin_Prefix method not found.");

            harmony.Patch(begin_method, prefix: new HarmonyMethod(begin_prefix));

            var get_icon_method = typeof(ASpawn).GetMethod("GetIcon") ?? throw new Exception("ASpawn.GetIcon method not found.");

            var get_icon_prefix = typeof(ClusterLaunchManifest).GetMethod("ASpawn_GetIcon_Prefix", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("ClusterLaunchManifest.ASpawn_GetIcon_Prefix method not found.");

            harmony.Patch(get_icon_method, prefix: new HarmonyMethod(get_icon_prefix));
        }

        private static bool ASpawn_GetIcon_Prefix(State s, ref Icon? __result, ASpawn __instance)
        {
            __result = null;
            if (__instance.thing is not ClusterMissile cluster)
                return true;
            if (Manifest.ClusterMissleToken?.Id == null)
            {
                __result = null;
                return false;
            }
            bool flipY = cluster.targetPlayer;
            if (__instance.fromPlayer && s.ship.Get(Status.backwardsMissiles) > 0)
                flipY = !flipY;
            __result = new Icon(cluster?.GetActionIcon() ?? (Spr)Manifest.ClusterMissleToken.Id, cluster?.stackSize ?? 0, Colors.textMain, flipY);
            return false;
        }

        private static bool ASpawn_Begin_Prefix(G g, State s, Combat c, ASpawn __instance)
        {
            Ship ship = __instance.fromPlayer ? s.ship : c.otherShip;
            if (!__instance.fromX.HasValue && ship.parts.FindIndex((Predicate<Part>)(p => p.type == PType.missiles && p.active)) == -1)
                return true;
            if (__instance.fromPlayer && g.state.ship.GetPartTypeCount(PType.missiles) > 1 && !__instance.multiBayVolley)
            {
                c.QueueImmediate((CardAction)new AVolleySpawnFromAllMissileBays()
                {
                    spawn = Mutil.DeepCopy<ASpawn>(__instance)
                });
                __instance.timer = 0.0;
                return false;
            }
            else
            {
                __instance.thing.fromPlayer = __instance.fromPlayer;

                foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                    __instance.thing = enumerateAllArtifact.ReplaceSpawnedThing(s, c, __instance.thing, __instance.fromPlayer);
                int worldX1 = __instance.GetWorldX(s, c);
                int worldX2 = worldX1;
                int num = worldX1 + __instance.offset;
                Part? partAtWorldX = ship.GetPartAtWorldX(worldX2);
                if (partAtWorldX != null)
                    partAtWorldX.pulse = 1.0;
                StuffBase? stuffBase;
                if (c.stuff.TryGetValue(num, out stuffBase))
                {
                    if (__instance.thing is ClusterMissile launched_cluster && stuffBase is ClusterMissile otherCluster)
                    {

                        if (__instance.isaacNamesIt)
                        {
                            List<string> list = __instance.thing.PossibleDroneNames().Where<string>((Func<string, bool>)(name => !s.storyVars.namedDronesSpawned.Contains(name))).ToList<string>();
                            if (list.Count != 0 && Mutil.NextRand() < 0.2)
                            {
                                __instance.thing.droneNameAccordingToIsaac = list[Mutil.NextRandInt() % list.Count];
                                s.storyVars.namedDronesSpawned.Add(__instance.thing.droneNameAccordingToIsaac);
                            }
                        }

                        if (__instance.fromPlayer)
                        {
                            foreach (Artifact enumerateAllArtifact in s.EnumerateAllArtifacts())
                                enumerateAllArtifact.OnPlayerSpawnSomething(s, c, __instance.thing);
                        }

                        ParticleBursts.DroneSpawn(num, __instance.fromPlayer);
                        if (ship.Get(Status.backwardsMissiles) > 0)
                            __instance.thing.targetPlayer = !__instance.thing.targetPlayer;
                        if (!__instance.thing.bubbleShield && ship.Get(Status.bubbleJuice) > 0)
                        {
                            __instance.thing.bubbleShield = true;
                            ship.Set(Status.bubbleJuice, ship.Get(Status.bubbleJuice) - 1);
                        }

                        c.fx.Add(new FlightFX()
                        {
                            start_x = worldX2,
                            start_y = launched_cluster.fromPlayer ? FlightFX.YRow.player : FlightFX.YRow.enemy,
                            target_x = num,
                            target_y = FlightFX.YRow.midrow,
                            target_flight_time = 0.35,
                            miss = false,
                            texture = SpriteLoader.Get(launched_cluster.GetIcon() ?? Spr.icons_recycle)
                        });
                        HandleClusterGrowth(otherCluster, s, c, launched_cluster);
                        return false;
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        private static void HandleClusterGrowth(ClusterMissile otherCluster, State s, Combat c, ClusterMissile launched_cluster)
        {
            //make feedback
            Audio.Play(FSPRO.Event.Drones_MissileLaunch);

            //grow other cluster
            otherCluster.GrowCluster(launched_cluster, c, s);
        }

    }
}
