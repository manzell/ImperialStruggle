using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class AddCardsToDeckCommand : Command
    {
        List<EventCard> cards;
        public AddCardsToDeckCommand(List<EventCard> cards) => this.cards = cards;
        public override void Do(GameAction action)
        {
            Debug.Log($"{cards.Count} added to Event Card Deck");
            cards.ForEach(card => Game.EventDeck.Push(card));
            Game.EventDeck = new(Game.EventDeck.OrderBy(card => Random.value)); // Consider adding a Shuffle Command separately
        }
    }
}