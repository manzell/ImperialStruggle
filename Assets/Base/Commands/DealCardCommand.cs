using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DealCardCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<Player> dealAction)
        {
            EventCard card = Game.eventDeck.First();

            dealAction.target.hand.Add(card);
            Game.eventDeck.Remove(card);

            Game.Log($"{card} dealt to {dealAction.target}");
        }
    }
}
