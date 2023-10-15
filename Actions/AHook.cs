namespace JohannaTheTrucker.Actions
{
    internal class AHook : CardAction
    {
        public bool fromPlayer = true;
        public bool hookToRight;

        public override void Begin(G g, State s, Combat c)
        {
            var move = CalculateMove(s, c);
            if (move == null)
            {
                return;
            }
            c.QueueImmediate(move);
        }

        public AMove? CalculateMove(State? s, Combat? c)
        {
            if (s == null || c == null)
                return null;
            //determine from where to hook
            Ship ship = this.fromPlayer ? s.ship : c.otherShip;
            var missile_bays = ship.parts.Where(e => e.type == PType.missiles && e.active);
            if (missile_bays.Count() == 0)
                return null;

            var hook_start = (hookToRight) ? missile_bays.Last() : missile_bays.First();

            var pos_x = ship.x + ship.parts.IndexOf(hook_start);

            //look through midrow to find target pos
            // var target_pos = c.stuff.Keys.OrderBy(e => e).Where(e => (hookToRight) ? e > pos_x : e < pos_x).Take(1);
            var target_pos = c.stuff.Keys.OrderBy(e => e).Where(e => (hookToRight) ? e > pos_x : e < pos_x);
            if (hookToRight)
                target_pos = target_pos.Take(1);
            else
                target_pos = target_pos.Reverse().Take(1);

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

        public override Icon? GetIcon(State s)
        {
            if (Manifest.HookIcon?.Id == null)
                return null;
            if (s.route is not Combat)
                return new Icon((Spr)(Manifest.HookIcon?.Id ?? throw new Exception("missing hook icon")), 0, Colors.textMain);
            if (hookToRight)
                return new Icon((Spr)(Manifest.HookRightIcon?.Id ?? throw new Exception("missing hook right icon")), 0, Colors.textMain);
            else
                return new Icon((Spr)(Manifest.HookLeftIcon?.Id ?? throw new Exception("missing hook left icon")), 0, Colors.textMain);
        }

        public override List<Tooltip> GetTooltips(State s)
        {
            var list = new List<Tooltip>();
            AMove? move;
            var glossary = new TTGlossary(Manifest.AHook_Glossary?.Head ?? throw new Exception("Missing AHook Glossary"));
            list.Add(glossary);
            if (s.route is Combat c && (move = CalculateMove(s, c)) != null)
            {
                var dir_key = hookToRight;
                list.AddRange(move.GetTooltips(s));
            }
            return list;
        }
    }
}