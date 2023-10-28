using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;

using HarmonyLib;

namespace JohannaTheTrucker
{
    public partial class Manifest
    {

        public static ExternalStatus? SmartExplosiveStatus { get; private set; }

        public static ExternalStatus? RocketSiloStatus { get; private set; }

        public static ExternalStatus? PopBubblesStatus { get; private set; }

        public static ExternalStatus? LoseDroneShiftStatus { get; private set; }

        public void LoadManifest(IStatusRegistry statusRegistry)
        {

            //patch in logic for our statuses
            var harmony = new Harmony("JohannaTheTrucker.Status");
            SmartExplosiveLogic(harmony);
            RocketSiloStatusLogic(harmony);
            PopBubblesStatusLogic(harmony);
            LoseDroneShiftLogic(harmony);

            //create status objects
            SmartExplosiveStatus = new ExternalStatus("JohannaTheTrucker.Status.SmartExplosive", true, Johanna_Primary_Color, null, SmartExplosiveSprite ?? throw new Exception("MissingSprite"), true);
            SmartExplosiveStatus.AddLocalisation("Smart Explosives", "Your missiles and cluster's won't fire if they would miss. Reduces by 1 at the start of your turn.");
            statusRegistry.RegisterStatus(SmartExplosiveStatus);

            RocketSiloStatus = new ExternalStatus("JohannaTheTrucker.Status.RocketSiloStatus", true, Johanna_Primary_Color, null, RocketSiloSprite ?? throw new Exception("MissingSprite"), true);
            RocketSiloStatus.AddLocalisation("Rocket Silo", "For each stack gain a Micro Missile card at the start of your turn.");
            statusRegistry.RegisterStatus(RocketSiloStatus);

            PopBubblesStatus = new ExternalStatus("JohannaTheTrucker.Status.PopBubblesStatus", true, Johanna_Primary_Color, null, PopBubblesSprite ?? throw new Exception("MissingSprite"), true);
            PopBubblesStatus.AddLocalisation("Bubble Pop", "At the start of your next turn all midrow bubbles are popped.");
            statusRegistry.RegisterStatus(PopBubblesStatus);

            LoseDroneShiftStatus = new ExternalStatus("JohannaTheTrucker.Status.LoseDroneShiftStatus", true, Johanna_Primary_Color, null, LoseDroneShiftSprite ?? throw new Exception("MissingSprite"), true);
            LoseDroneShiftStatus.AddLocalisation("Lose Drone Shift", "Lose all drone shift at the start of your next turn.");
            statusRegistry.RegisterStatus(LoseDroneShiftStatus);
        }

        private void LoseDroneShiftLogic(Harmony harmony)
        {

            var start_turn_method = typeof(Ship).GetMethod("OnBeginTurn") ?? throw new Exception("Couldnt find Ship.OnBeginTurn method");
            var start_turn_post = typeof(Manifest).GetMethod("LoseDroneShiftTurnStart", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("Couldnt find Manifest.LoseDroneShiftTurnStart method");
            harmony.Patch(start_turn_method, postfix: new HarmonyMethod(start_turn_post));
        }

        private static void LoseDroneShiftTurnStart(Ship __instance)
        {
            if (LoseDroneShiftStatus?.Id == null)
                return;
            var status = (Status)LoseDroneShiftStatus.Id;
            if (__instance.Get(status) <= 0)
                return;
            __instance.Set(Status.droneShift, 0);
            __instance.Add(status, -1);
        }


        private void SmartExplosiveLogic(Harmony harmony)
        {
            //patch start turn to decrease status by 1.

            var start_turn_method = typeof(Ship).GetMethod("OnBeginTurn") ?? throw new Exception("Couldnt find Ship.OnBeginTurn method");
            var start_turn_post = typeof(Manifest).GetMethod("SmartExplosiveTurnStart", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("Couldnt find Manifest.SmartExplosiveTurnStart method");
            harmony.Patch(start_turn_method, postfix: new HarmonyMethod(start_turn_post));

            //patch regular missile.begin to not fire if the owner has smart explosive

            var a_missile_hit_update = typeof(AMissileHit).GetMethod("Update") ?? throw new Exception("Couldnt find AMissileHit.Update method");

            var a_missile_hit_prefix = typeof(Manifest).GetMethod("SmartExplosiveMissileLock", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("Couldnt find Manifest.SmartExplosiveMissileLock method");
            harmony.Patch(a_missile_hit_update, prefix: new HarmonyMethod(a_missile_hit_prefix));
        }

        private static bool SmartExplosiveMissileLock(State s, Combat c, AMissileHit __instance)
        {
            if (SmartExplosiveStatus?.Id == null)
                return true;
            if (!c.stuff.TryGetValue(__instance.worldX, out var stuffBase) || stuffBase is not Missile missile)
                return true;

            var ship = missile.fromPlayer ? s.ship : c.otherShip;
            if (ship.Get((Status)SmartExplosiveStatus.Id) <= 0)
                return true;
            //check if seeker missile
            if (missile.missileType == MissileType.seeker)
                return true;
            //check if missile will hit
            var target_ship = missile.targetPlayer ? s.ship : c.otherShip;
            if (target_ship.HasNonEmptyPartAtWorldX(__instance.worldX))
                return true;
            //if not kill action immediately and let update not run.
            __instance.timer = -1;
            return false;
        }

        private static void SmartExplosiveTurnStart(Ship __instance)
        {
            if (SmartExplosiveStatus?.Id == null)
                return;
            __instance.Add((Status)SmartExplosiveStatus.Id, -1);
        }

        private void RocketSiloStatusLogic(Harmony harmony)
        {
            // patch turn start of player to generate card in hand.
            var a_start_player_turn_begin_method = typeof(AStartPlayerTurn).GetMethod("Begin") ?? throw new Exception("Couldnt find AStartPlayerTurn.Begin method");
            var a_start_player_turn_begin_postfix = typeof(Manifest).GetMethod("RocketSiloTurnStart", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("Couldnt find Manifest.RocketSiloTurnStart method");
            harmony.Patch(a_start_player_turn_begin_method, postfix: new HarmonyMethod(a_start_player_turn_begin_postfix));
        }

        private static void RocketSiloTurnStart(State s, Combat c)
        {
            if (RocketSiloStatus?.Id == null)
                return;
            var status = (Status)RocketSiloStatus.Id;
            var ammount = s.ship.Get(status);
            if (ammount <= 0)
                return;
            s.ship.PulseStatus(status);
            c.QueueImmediate(new AAddCard()
            {
                amount = ammount,
                card = new Cards.MicroMissiles(),
                handPosition = 0,
                destination = CardDestination.Hand
            });
        }

        private void PopBubblesStatusLogic(Harmony harmony)
        {
            //patch turn start to decrease this value by 1 and destroy all midrow bubbles.
            var start_turn_method = typeof(Ship).GetMethod("OnBeginTurn") ?? throw new Exception("Couldnt find Ship.OnBeginTurn method");
            var start_turn_post = typeof(Manifest).GetMethod("PopBubblesTurnStart", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic) ?? throw new Exception("Couldnt find Manifest.PopBubblesTurnStart method");
            harmony.Patch(start_turn_method, postfix: new HarmonyMethod(start_turn_post));
        }

        private static void PopBubblesTurnStart(Combat c, Ship __instance)
        {
            if (PopBubblesStatus?.Id == null)
                return;
            var status = (Status)PopBubblesStatus.Id;
            if (__instance.Get(status) <= 0)
                return;
            Audio.Play(FSPRO.Event.Hits_ShieldPop);
            foreach (var stuff in c.stuff.Values)
            {
                stuff.bubbleShield = false;
            }
            // __instance.Add(status, -1);
            __instance.Set(status, 0);
        }

    }
}
