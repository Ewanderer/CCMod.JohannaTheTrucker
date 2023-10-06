using CobaltCoreModding.Definitions.ExternalItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Actions
{
    internal class AHook : CardAction
    {

        public bool fromPlayer = true;
        public bool hookToRight;


        private AMove? CalculateMove(State? s, Combat? c)
        {
            if (s == null || c == null)
                return null;
            //determine from where to hook
            Ship ship = this.fromPlayer ? s.ship : c.otherShip;
            var missile_bays = ship.parts.FindAll(e => e.type == PType.missiles && e.active);
            if (missile_bays.Count == 0)
                return null;

            var hook_start = (hookToRight) ? missile_bays.Last() : missile_bays.First();

            var pos_x = ship.x + ship.parts.IndexOf(hook_start);

            //look through midrow to find target pos
            var target_pos = c.stuff.Keys.OrderBy(e => e).Where(e => (hookToRight) ? e > pos_x : e < pos_x).Take(1);
            //check if we found a target.
            if (!target_pos.Any())
                return null;
            //align with missile bay
            var target_x = target_pos.First();
            var move_action = new AMove()
            {
                dir = target_x - pos_x,
                fromEvade = false,
                ignoreHermes = true,
                ignoreFlipped = true,
                targetPlayer = fromPlayer,
                whoDidThis = this.whoDidThis
            };

            return move_action;
        }

        public override void Begin(G g, State s, Combat c)
        {
            var move = CalculateMove(s, c);
            if (move == null)
                return;
            c.QueueImmediate(move);
        }

        public override Icon? GetIcon(State s)
        {
            if (Manifest.HookIcon?.Id == null)
                return null;
            return new Icon((Spr)Manifest.HookIcon.Id, 0, Colors.textMain, hookToRight);
        }

        public override List<Tooltip> GetTooltips(State s)
        {
            var list = new List<Tooltip>();
            AMove? move;
            list.Add(new TTGlossary("JohannaTheTruckerAHook"));
            if (s.route is Combat c && (move = CalculateMove(s, c)) != null)
            {
                var dir_key = hookToRight;
                list.AddRange(move.GetTooltips(s));
            }
            return list;
        }

    }
}
