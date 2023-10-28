using JohannaTheTrucker.Actions;
using JohannaTheTrucker.MidrowStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.rare, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class FireFireFire : Card
    {

        public override List<CardAction> GetActions(State s, Combat c)
        {
            var result = new List<CardAction>();

            foreach (var missile in c.stuff.Values.Where(e => e is Missile))
            {
                if (missile.fromPlayer)
                    result.AddRange(missile.GetActions(s, c) ?? new List<CardAction>());
            }
            result.Add(new AExhaustAllCluster()
            {
                fire_only_once = upgrade == Upgrade.B,
                fromPlayer = true,
                canRunAfterKill = true,
            });

            return result;
        }

        public override CardData GetData(State state)
        {
            int cost;
            string desc;
            switch (upgrade)
            {
                case Upgrade.None:
                    cost = 3;
                    desc = Loc.GetLocString(Manifest.FireFireFireCard?.DescLocKey ?? throw new Exception("Card not found."));
                    break;
                case Upgrade.A:
                    cost = 4;
                    desc = Loc.GetLocString(Manifest.FireFireFireCard?.DescLocKey ?? throw new Exception("Card not found."));
                    break;
                case Upgrade.B:
                    cost = 1;
                    desc = Loc.GetLocString(Manifest.FireFireFireCard?.DescBLocKey ?? throw new Exception("Card not found."));
                    break;
                default:
                    throw new NotImplementedException($"Unkown upgrade: {upgrade}");
            }

            return new CardData
            {
                cost = cost,
                exhaust = upgrade != Upgrade.B,
                retain = upgrade == Upgrade.A,
                recycle = upgrade == Upgrade.B,
                description = desc
            };
        }

        public override string Name() => "Fire! Fire! Fire!";

    }
}
