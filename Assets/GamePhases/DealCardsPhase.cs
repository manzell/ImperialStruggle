using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class DealCardsPhase : MonoBehaviour
{
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

        //for (int i = 0; i < cardsToDeal; i++)
        //    foreach(Player player in players)
        //        phase.gameActions.Add(new DrawCard(player)); 

        callback.Invoke(); 
    }
}