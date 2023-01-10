using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class RemoveWarTileCommand : Command
    {
        WarTile tile;

        public RemoveWarTileCommand(WarTile tile)
        {
            this.tile = tile;
        }

        public override void Do(GameAction action)
        {
            foreach(Theater theater in Game.NextWarTurn.theaters)
                if(theater.warTiles.Remove(tile))
                    Debug.Log($"{tile.Name} removed from {theater.Name}");

            tile.faction.player.BonusWarTiles.Add(tile);
        }
    }
}
