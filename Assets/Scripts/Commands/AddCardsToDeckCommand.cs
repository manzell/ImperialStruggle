using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddCardsToDeckCommand : Command
{
    List<EventCardData> cards; 
    public AddCardsToDeckCommand(List<EventCardData> cards) => this.cards = cards;
    public override void Do(GameAction action)
    {
        cards.ForEach(card => Game.EventDeck.Push(card));
        Game.EventDeck = new(Game.EventDeck.OrderBy(card => Random.value)); // Consider adding a Shuffle Command separately
    }
}
