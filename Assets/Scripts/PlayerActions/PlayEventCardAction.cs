using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PlayEventCardAction : PlayerAction
{
    UnityAction callback;
    protected override void Do()
    {
        SelectionController selectionController = FindObjectOfType<SelectionController>();
        List<EventCard> eventCards = actingPlayer.hand.Where(card =>
            card.reqdActionType == ActionPoint.ActionType.None || card.reqdActionType == Phase.CurrentPhase.GetComponent<ActionRound>()?.investmentTile.majorActionType).ToList();

        if (eventCards.Count > 0)
        {
            SelectionController.Selection selector = selectionController.Select(eventCards.ToList<ISelectable>(), 1);

            selector.SetTitle($"Select a {actingPlayer.faction} Event Card to Play");
            selector.callback = Finish;
        }
    }

    public void Finish(List<ISelectable> selectedEventCards)
    {
        if(selectedEventCards.Count > 0)
        {
            EventCard eventCard = selectedEventCards.First() as EventCard;
            Debug.Log($"{actingPlayer.faction} plays {eventCard.name}");
        }
    }
}
