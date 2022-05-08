using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DeckPhaseCommand : Command
{
    [SerializeField] List<EventCard> cards;

    public override void Do(BaseAction action)
    {
        Game.eventDeck.AddRange(cards);
        Game.eventDeck.OrderBy(card => Random.value);
    }
}
