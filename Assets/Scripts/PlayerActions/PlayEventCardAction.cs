using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class PlayEventCardAction : PlayerAction
    {
        public static System.Action<EventCard> PlayEventCardEvent;
        public static System.Action<Selection<EventCard>> SelectEventCardsEvent; 

        protected override async Task Do(IAction context)
        {
            IEnumerable<EventCard> eventCards = Player.Cards.Where(card =>
                card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionPoint.type);

            Selection<EventCard> selection = new(Player, eventCards); 
            selection.SetTitle($"Select a {Player.Faction} Event Card to Play");

            await selection.Completion; 
            
            if (selection.FirstOrDefault() is EventCard card)
            {
                PlayEventCardEvent?.Invoke(card);
                Debug.Log($"{selection.player.Faction} plays {card} <Not Implemented>");
            }
        }
    }

    public class PlayEventCardResponse : SelectionReceiver<EventCard>
    {
        public override void OnSelect(Selection<EventCard> selection)
        {
            if (selection.FirstOrDefault() is EventCard card)
            {
                Debug.Log($"{selection.player.Faction} plays {card} <Not Implemented>");

            }
        }
    }
}