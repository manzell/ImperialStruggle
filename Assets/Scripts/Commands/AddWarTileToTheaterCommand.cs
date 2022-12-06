using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class AddWarTileToTheaterCommand : Command
{
    WarTile tile;
    Theater theater; 

    public AddWarTileToTheaterCommand(WarTile tile, Theater theater)
    {
        this.tile = tile;
        this.theater = theater;
    }

    public override void Do(GameAction action) => theater.warTiles.Add(tile); 
}