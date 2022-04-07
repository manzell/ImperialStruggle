using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DrawCard : Command
{
    public static UnityEvent<DrawCard> drawCardEvent = new UnityEvent<DrawCard>();
    public EventCard card;
    public Game.Faction faction; 

    public DrawCard(Player player)
    {
        card = Game.eventDeck.First();
        faction = player.faction; 
        Do(player.faction); 
    }

    public override void Do(Game.Faction faction)
    {
        Player player = Player.players[faction]; 

        player.hand.Add(card);
        Game.eventDeck.Remove(card);

        Debug.Log($"{card} dealt to {player}");

        drawCardEvent.Invoke(this);
    }
}