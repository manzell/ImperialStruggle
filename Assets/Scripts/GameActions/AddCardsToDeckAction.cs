using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class AddCardsToDeckAction : GameAction
{
    [SerializeField] List<EventCard> cards;

    protected override void Do()
    {
        foreach (EventCard card in cards)
            commands.Add(new AddCardToDeckCommand(card)); 
    }
}
