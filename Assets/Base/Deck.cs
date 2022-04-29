using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Deck<T> : List<IDeckItem>
{
    int countRemaining => this.Count(tile => tile.status == IDeckItem.DeckStatus.InDeck);
    int maxAvailable => this.Count(tile => tile.status == IDeckItem.DeckStatus.InDeck || tile.status == IDeckItem.DeckStatus.Discarded);

    public T Draw()
    {
        if (countRemaining == 0)
            Reshuffle(); 

        return (T)this.First(tile => tile is T); 
    }

    public void Reshuffle()
    {

        foreach (IDeckItem tile in this.Where(tile => tile.status == IDeckItem.DeckStatus.Discarded))
            tile.status = IDeckItem.DeckStatus.InDeck;
    }
}

public interface IDeckItem
{
    public enum DeckStatus { InDeck, Held, Discarded, Removed }
    public DeckStatus status {  get; set; }
}