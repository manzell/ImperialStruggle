using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PlayEventCardPhase : MonoBehaviour, IPhaseAction
{
    UnityAction callback;
    public List<EventCard> eventableCards = new List<EventCard>();

    public void Do(Phase phase, UnityAction callback)
    {
        ActionRound actionRound = GetComponent<ActionRound>();
        InvestmentTile tile = actionRound.investmentTile;
        this.callback = callback;

        Debug.Log(this);

        //foreach (EventCard card in Player.players[actionRound.actingFaction].hand)
        //    if (card.reqdActionType == Game.ActionType.None || Player.players[actionRound.actingFaction].majorActionPoints.ContainsKey(card.reqdActionType))
        //        eventableCards.Add(card);

        eventableCards = Player.players[actionRound.actingFaction].hand
            .Where(card => tile.eventCardTrigger == true && (card.reqdActionType == Game.ActionType.None || card.reqdActionType == tile.majorAction.Keys.ToList()[0]))
            .ToList();

        if(tile.eventCardTrigger == false)
        {
            Debug.Log($"{tile} does not allow an Event Card to be played. Skipping {this}");
            callback.Invoke();
        }    
        else if(eventableCards.Count == 0)
        {
            Debug.Log($"No Eventable Cards available with {tile}. Skipping {this}");
            callback.Invoke(); 
        }
    }

    [Sirenix.OdinInspector.Button(Name ="Play Card")]
    public void PlayCard(EventCard card)
    {
        Debug.Log($"{GetComponent<ActionRound>().actingFaction} plays {card}.");
        card.PlayCard(callback);
    }

    [Sirenix.OdinInspector.Button(Name = "Resolve Phase")]
    public void FinishCardPlay()
    {
        Debug.Log($"{ GetComponent<ActionRound>().actingFaction} does not play an Event Card");
        callback.Invoke();
    }
}
