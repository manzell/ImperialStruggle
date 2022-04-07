using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class PlayCard : Command
{
    public static UnityEvent<PlayCard> playCardEvent = new UnityEvent<PlayCard>(); 
    public EventCard card;
    Game.Faction faction; 

    public PlayCard(Game.Faction faction, EventCard card)
    {
        this.faction = faction;
        this.card = card;
        Do(faction); 
    }

    public override void Do(Game.Faction faction)
    {
        card.Play(() => Finish(faction));
    }

    void Finish(Game.Faction faction)
    {
        Player.players[faction].hand.Remove(card);
        playCardEvent.Invoke(this);
    }

    public override void Undo()
    {
        Player.players[faction].hand.Add(card);
    }
}
