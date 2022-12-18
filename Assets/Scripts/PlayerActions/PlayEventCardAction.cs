using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class PlayEventCardAction : PlayerAction
    {
        protected override Task Do()
        {
            return Task.CompletedTask; 
            /*
            List<EventCard> eventCards = actingPlayer.hand.Where(card =>
                card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionType).ToList();

            if (eventCards.Count > 0)
            {
                SelectionController<EventCard>.Selection selector = new(eventCards, Finish);
                selector.SetTitle($"Select a {actingPlayer.faction} Event Card to Play");
            }
            */
        }

        public void Finish(EventCard card)
        {
            Debug.Log($"{Player.Faction} plays {card}");
        }
    }
}