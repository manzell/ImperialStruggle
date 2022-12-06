using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddCardToDeckCommand : Command
{
    EventCard card; 
    public AddCardToDeckCommand(EventCard card) => this.card = card; 
    public override void Do(GameAction action) => Game.eventDeck.Add(card);
}
