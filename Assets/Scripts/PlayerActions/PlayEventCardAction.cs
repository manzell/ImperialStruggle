using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class PlayEventCardAction : PlayerAction
    {
        public static System.Action<Selection<EventCard>> SelectEventCardEvent; 

        public virtual IEnumerable<EventCard> GetEligibleEventCards()
        {
            return Player.Cards.Where(card =>
                card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionPoint.type);
        }

        protected override Task Do()
        {
            IEnumerable<EventCard> eventCards = GetEligibleEventCards(); 

            Selection<EventCard> selection = new(Player, eventCards, Finish); 
            selection.SetTitle($"Select a {Player.Faction} Event Card to Play");

            SelectEventCardEvent?.Invoke(selection); 

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