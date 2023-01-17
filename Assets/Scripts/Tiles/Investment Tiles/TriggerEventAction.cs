using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class TriggerEventCardAction : PlayerAction
    {
        protected override async Task Do(IAction context)
        {
            if(Phase.CurrentPhase is ActionRound actionRound)
            {
                ActionPoint.ActionType investmentActionType = actionRound.investmentTile.majorActionPoint.type;
                IEnumerable<EventCard> eligibleCards = Player.Cards.Where(card => card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == investmentActionType);

                if (eligibleCards.Count() > 0)
                {
                    Selection<EventCard> selection = new (Player, eligibleCards, Finish);
                    selection.SetTitle("You may play an Event Card");

                    await selection.Completion; 
                }
            }
        }

        async void Finish(Selection<EventCard> cards)
        {
            if(cards.Count() > 0)
            {
                EventCard card = cards.First();
                Faction actingFaction = card.cardActions.ContainsKey(Player.Faction) ? Player.Faction : Game.Neutral;
                PlayerAction baseAction = card.cardActions[actingFaction].baseAction;
                PlayerAction bonusAction = card.cardActions[actingFaction].bonusAction;

                bool bonusEligible = card.bonusCondition.Check(this);

                if (baseAction != null)
                    await baseAction.Execute(this);

                if (bonusEligible && bonusAction != null)
                    await bonusAction.Execute(this);
            }
        }
    }
}