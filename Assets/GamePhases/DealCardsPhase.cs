using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DealCardsPhase : MonoBehaviour, IPhaseAction
{
    public static UnityEvent<Game.Faction, EventCard> dealCardEvent = new UnityEvent<Game.Faction, EventCard>();

    public int cardsToDeal = 3;

    public void Do(Phase phase, UnityAction callback)
    {
        List<Player> players = FindObjectsOfType<Player>().ToList(); 

        if(Game.eventDeck.Count < players.Count * cardsToDeal)
        {
            Debug.Log($"Re-adding {Game.eventDiscards.Count} discarded Event {(Game.eventDiscards.Count == 1 ? "Card" : "Cards")} into the Event Deck"); 
            Game.eventDeck.AddRange(Game.eventDiscards); 
            Game.eventDiscards.Clear();
        }

        Game.eventDeck = Game.eventDeck.OrderBy(card => Random.value).ToList();

        for (int i = 0; i < cardsToDeal; i++)
        {
            foreach(Player player in players)
            {
                EventCard card = Game.eventDeck.First();
                player.hand.Add(card); 
                Game.eventDeck.Remove(card);

                dealCardEvent.Invoke(player.faction, card); 
                Debug.Log($"{card} dealt to {player}"); 
            }
        }

        foreach(Player player in players)
        {
            if(player.hand.Count > cardsToDeal)
            {
                Debug.Log($"{player} has {player.hand.Count} {(player.hand.Count == 1 ? "card" : "cards")} and must discard down");
            }
        }

        callback.Invoke(); 
    }
}