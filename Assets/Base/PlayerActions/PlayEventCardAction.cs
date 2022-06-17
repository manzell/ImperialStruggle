using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PlayEventCardAction : PlayerAction
{
    UnityAction callback;
    public override void Do(UnityAction callback)
    {
        SelectionController selectionController = FindObjectOfType<SelectionController>();
        List<EventCard> eventCards = player.hand.Where(card =>
            card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.currentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionType).ToList();

        this.callback = callback;

        if (eventCards.Count > 0)
        {
            SelectionController.Selection selector = selectionController.Select(eventCards.ToList<ISelectable>(), 1);

            selector.SetTitle($"Select a {player.faction} Event Card to Play");
            selector.callback = Finish;
        }
        else
        {
            callback.Invoke(); 
        }
    }

    public void Finish(List<ISelectable> selectedEventCards)
    {
        if(selectedEventCards.Count > 0)
        {
            EventCard eventCard = selectedEventCards.First() as EventCard;
            Debug.Log($"{player.faction} plays {eventCard.name}");
        }
        callback.Invoke(); 
    }
}
