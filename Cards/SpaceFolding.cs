using JohannaTheTrucker.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.uncommon, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class SpaceFolding : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            //get missile bay pivot
            var result = new List<CardAction>();

            foreach (var missile_bay in s.ship.parts.Where(e => e.active && e.type == PType.missiles))
            {
                var pivot = s.ship.parts.IndexOf(missile_bay) + s.ship.x;
                result.Add(new AFlipMidrow() { pivot_world_x = pivot });
            }

            if (upgrade == Upgrade.A)
            {
                result.Add(new AStatus()
                {
                    status = Status.droneShift,
                    statusAmount = 2,
                    targetPlayer = true
                });
            }
            else if (upgrade == Upgrade.B)
            {
                result.Add(new ABubbleField());
            }

            return result;
        }

        public override CardData GetData(State state)
        {
            string desc="Unkown Upgrade"; 
            switch (upgrade)
            {
                case Upgrade.None:
                    desc = Loc.GetLocString(Manifest.SpaceFoldingCard?.DescLocKey??throw new Exception("Missing card"));
                    break;
                case Upgrade.A:
                    desc = Loc.GetLocString(Manifest.SpaceFoldingCard?.DescALocKey ?? throw new Exception("Missing card"));
                    break;
                case Upgrade.B:
                    desc = Loc.GetLocString(Manifest.SpaceFoldingCard?.DescBLocKey ?? throw new Exception("Missing card"));
                    break;
            }
            return new CardData()
            {
                cost = upgrade == Upgrade.B ? 2 : 1,
                description = desc
            };
        }

        public override string Name() => "Space Folding";

    }
}
