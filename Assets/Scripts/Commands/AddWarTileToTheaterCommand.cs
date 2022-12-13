using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class AddWarTileToTheaterCommand : Command
    {
        WarTile tile;
        Theater theater;

        public AddWarTileToTheaterCommand(WarTile tile, Theater theater)
        {
            this.tile = tile;
            this.theater = theater;
        }

        public override void Do(GameAction action)
        {
            theater.warTiles.Add(tile);
            Debug.Log($"{tile.Name} added to {theater.Name}");
        }
    }
}