using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class AddCardsToDeck : MonoBehaviour, IPhaseAction
{
    [SerializeField] List<EventCard> cards; 
    public void Do(Phase phase, UnityAction callback)
    {
        Game.eventDeck.AddRange(cards);
        Game.eventDeck.OrderBy(card => Random.value);

        Debug.Log($"{cards.Count} {(cards.Count == 1 ? "card" : "cards")} added to Event Deck");

        callback.Invoke(); 
    }
}
