using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddCardToDeckCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<EventCard> cardAction)
        {
            Game.eventDeck.Add(cardAction.target);
        }
    }
}
