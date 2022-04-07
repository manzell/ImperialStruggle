using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DeckPhase : Command
{
    public List<EventCard> newCards;

    List<EventCard> previousDeck;

    public override void Do(Game.Faction f)
    {
        previousDeck = new List<EventCard>(Game.eventDeck); 
        Game.eventDeck.AddRange(newCards);
        Game.eventDeck.OrderBy(card => Random.value); 
    }

    public override void Undo()
    {
        Game.eventDeck = previousDeck; 
    }
}
