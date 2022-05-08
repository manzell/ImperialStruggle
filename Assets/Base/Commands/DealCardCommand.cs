using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DealCardCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is DealCardsAction dealAction)
        {
            EventCard card = Game.eventDeck.First();
            Player player = Player.players[dealAction.faction]; 

            player.hand.Add(card);
            Game.eventDeck.Remove(card);

            Game.Log($"{card} dealt to {player.faction}");
        }
    }
}
