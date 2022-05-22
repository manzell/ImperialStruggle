using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class DealCardCommand : Command
{
    public static UnityEvent<EventCard> dealCardEvent = new UnityEvent<EventCard>(); 

    public override void Do(BaseAction action)
    {
        if(action is ITargetType<Player> dealAction)
        {
            EventCard card = Game.eventDeck.First();
            Game.eventDeck.Remove(card);
            dealAction.target.hand.Add(card);
            dealCardEvent.Invoke(card); 

            Game.Log($"{card} dealt to {dealAction.target.name}");
        }
    }
}
