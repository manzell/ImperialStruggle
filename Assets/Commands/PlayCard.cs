using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PlayCard : Command
{
    public static UnityEvent<PlayCard> playCardEvent = new UnityEvent<PlayCard>(); 
    public EventCard card;

    public override void Do(Action action)
    {
        //card.Play(() => Finish(action));
    }

    void Finish(Game.Faction faction)
    {
        Player.players[faction].hand.Remove(card);
        playCardEvent.Invoke(this);
    }

    public override void Undo()
    {
        //Player.players[actingFaction].hand.Add(card);
    }
}
