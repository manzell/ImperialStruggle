using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class MilitaryUpgradeAction : PlayerAction
    {
        protected override void Do()
        {
            WarTile drawnWarTile = player.warTiles.Dequeue(); 
            IEnumerable<WarTile> eligibleTiles = Game.NextWarTurn.theaters.SelectMany(theater => 
                theater.warTiles.Where(tile => tile.faction == player.faction && tile.warTileSet == WarTile.WarTileSet.Basic));

            // TODO - Also display the Drawn Tile to the player. Probably need a custom receiver on Selection
            new Selection<WarTile>(player, drawnWarTile, eligibleTiles, Finish);

            void Finish(Theater theater, WarTile replacedTile, WarTile newTile)
            {
                theater.warTiles.Remove(replacedTile);
                theater.warTiles.Add(newTile);
            }
        }
    }
}