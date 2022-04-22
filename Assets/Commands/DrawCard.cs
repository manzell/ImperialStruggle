using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DrawCard : Command
{
    public static UnityEvent<DrawCard> drawCardEvent = new UnityEvent<DrawCard>();
    
    public Game.Faction targetFaction;

    public override void Do(Action action)
    {
        Player player = Player.players[targetFaction];
        EventCard card = Game.eventDeck.OrderBy(eventCard => Random.value).First(); 

        player.hand.Add(card);
        Game.eventDeck.Remove(card);

        Debug.Log($"{card} dealt to {player}");

        drawCardEvent.Invoke(this);
    }
}