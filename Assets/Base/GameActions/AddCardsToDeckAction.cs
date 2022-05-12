using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class AddCardsToDeckAction : GameAction, ITargetType<EventCard>
{
    [SerializeField] List<EventCard> cards;

    EventCard card; 
    public EventCard target => card;

    protected override void Do(UnityAction callback)
    {
        foreach(EventCard eventCard in cards)
        {
            card = eventCard;
            base.Do(() => { });
        }

        Game.eventDeck = Game.eventDeck.OrderBy(card => Random.value).ToList();

        callback.Invoke(); 
    }
}
