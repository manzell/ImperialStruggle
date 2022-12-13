using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class TriggerEventAction : PlayerAction
    {
        protected override void Do()
        {
            if(Phase.CurrentPhase is ActionRound actionRound)
            {
                ActionPoint.ActionType investmentActionType = actionRound.investmentTile.majorActionPoint.actionType;
                IEnumerable<EventCard> eligibleCards = player.hand.Where(card => card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == investmentActionType);

                if (eligibleCards.Count() > 0)
                    new Selection<EventCard>(player, eligibleCards, Finish);

                void Finish(Selection<EventCard> selection)
                {
                    EventCard card = selection.First();
                    Faction actingFaction = card.cardActions.ContainsKey(player.faction) ? player.faction : Game.Neutral;

                    GameAction baseAction = card.cardActions[actingFaction].baseAction; 
                    GameAction bonusAction = card.cardActions[actingFaction].bonusAction;

                    // TODO - Learn how to Queue Actions and then have a single iterator that works on the Queue and waits for a finish beep from the 

                    if (baseAction != null) 
                        baseAction.Execute();

                    if (bonusAction != null && card.bonusCondition.Test(null))
                        bonusAction.Execute(); 
                }
            }
        }
    }
}