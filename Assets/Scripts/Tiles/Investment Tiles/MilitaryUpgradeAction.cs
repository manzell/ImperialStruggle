using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class MilitaryUpgradeAction : PlayerAction
    {
        protected async override Task Do()
        {
            WarTile drawnWarTile = Player.WarTiles.OrderBy(x => Random.value).FirstOrDefault(); 
            IEnumerable<WarTile> eligibleTiles = Game.NextWarTurn.theaters.SelectMany(theater => 
                theater.warTiles.Where(tile => tile.faction == Player.Faction && tile.warTileSet == WarTile.WarTileSet.Basic));

            Debug.LogWarning("TODO Also display the Drawn Tile to the player. Probably need a custom receiver on Selection"); 
            await new Selection<WarTile>(Player, drawnWarTile, eligibleTiles, Finish).Completion; 
           
            void Finish(Theater theater, WarTile replacedTile, WarTile newTile)
            {
                theater.warTiles.Remove(replacedTile);
                theater.warTiles.Add(newTile);
            }
        }
    }
}