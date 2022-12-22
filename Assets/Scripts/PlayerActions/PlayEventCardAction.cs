using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class PlayEventCardAction : PlayerAction
    {
        protected override Task Do()
        {
            IEnumerable<EventCard> eventCards = Player.Cards.Where(card =>
                card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionPoint.type);

            if (eventCards.Count() > 0)
            {
                Selection<EventCard> selection = new(Player, eventCards, Finish); 
                selection.SetTitle($"Select a {Player.Faction} Event Card to Play");
            }

            return Task.CompletedTask;
        }

        public void Finish(Selection<EventCard> selection)
        {
            if(selection.Count() > 0)
            {
                Debug.Log($"{Player.Faction} plays {selection.selectedItems.First()} <Not Implemented>");
                
            }
        }
    }
}