using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class BigSwing : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var list = new List<CardAction>();
            var hook_action = new AHook()
            {
                hookToRight = flipped
            };

            hook_action.disabled = hook_action.CalculateMove(s, c) == null;

            var distance = hook_action.CalculateMove(s, c)?.dir ?? 0;

            list.Add(new AMove()
            {
                dir = distance*2,
                targetPlayer = true,
                fromEvade = false,
                disabled = distance == 0,
            });


            return list;
        }

        public override CardData GetData(State state)
        {

            var hook_action = new AHook()
            {
                hookToRight = flipped
            };

            int distance = 0;
            if (state.route is Combat c)
            {

                hook_action.disabled = hook_action.CalculateMove(state, c) == null;

                distance = Math.Abs(hook_action.CalculateMove(state, c)?.dir ?? 0)*3;
            }

            int cost;
            switch (upgrade)
            {
                case Upgrade.None:
                    cost = 2;
                    break;
                case Upgrade.A:
                    cost = 1;
                    break;
                case Upgrade.B:
                    cost = 0;
                    break;
                default:
                    throw new NotImplementedException($"Uknown upgrade value: {upgrade}");
            }

            var dir_str = flipped ? Loc.GetLocString("env.MSolarWind.desc.right") : Loc.GetLocString("env.MSolarWind.desc.left");

            return new CardData()
            {
                cost = cost,
                retain = upgrade == Upgrade.B,
                flippable = true,
                exhaust = upgrade == Upgrade.B,
                description = string.Format(Loc.GetLocString(Manifest.BigSwingCard?.DescLocKey ?? throw new Exception("Missing Big swing card")), dir_str, distance)
            };
        }


        public override string Name() => "Big Swing";
    }
}
