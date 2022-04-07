using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWarTile : Command
{
    WarTile tile;
    Theater theater; 

    public AddWarTile(WarTile warTile, Theater theater)
    {
        tile = warTile;
        this.theater = theater;
        Do(tile.faction); 
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction}: {tile} added to {theater}");
        theater.warTiles.Add(tile);
        theater.onPhaseActions.Add(tile); 
    }
}