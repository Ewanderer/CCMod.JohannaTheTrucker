using JohannaTheTrucker.Actions;

namespace JohannaTheTrucker.Cards
{
    [CardMeta(rarity = Rarity.common, upgradesTo = new Upgrade[] { Upgrade.A, Upgrade.B })]
    public class ReelIn : Card
    {
        public override List<CardAction> GetActions(State s, Combat c)
        {
            var list = new List<CardAction>();
            var hook_action = new AHook()
            {
                hookToRight = flipped
            };

            hook_action.disabled = hook_action.CalculateMove(s, c) == null;

            list.Add(hook_action);
            if (upgrade != Upgrade.B)
            {

                list.Add(new ADroneMove()
                {
                    dir = -1
                });
            }
            return list;
        }

        public override CardData GetData(State state)
        {
            return new CardData()
            {
                cost = upgrade != Upgrade.A ? 1 : 0,
                retain = upgrade == Upgrade.B,
                flippable = true,
            };
        }

        public override string Name() => "Reel In";
    }
}