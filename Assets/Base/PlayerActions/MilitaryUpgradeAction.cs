using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class MilitaryUpgradeAction : PlayerAction, ITargetType<WarTile>, ITargetType<WarTurn>
{
    WarTile ITargetType<WarTile>.target => warTile;
    WarTurn ITargetType<WarTurn>.target => nextWar;

    WarTile warTile;
    WarTurn nextWar; 

    protected override void Do(UnityAction callback)
    {   
        List<WarTile> warTiles = new List<WarTile>();
        nextWar = Game.NextWarTurn;
        
        nextWar.theaters.ForEach(theater => warTiles.AddRange(theater.warTiles[player.faction].Where(tile => tile.warTileSet == WarTile.WarTileSet.Basic).ToList()));

        if(warTiles.Count > 0)
        {
            SelectionController.Selection<WarTile> selection = FindObjectOfType<SelectionController>().Select(warTiles, 1);
            selection.SetTitle($"Select a {player.faction} Basic War Tile to replace");
            selection.callback = selectedTiles => Finish(selectedTiles, callback);
        }
        else
        {
            callback.Invoke(); 
        }
    }

    void Finish(List<WarTile> selectedTiles, UnityAction callback)
    {
        if(selectedTiles.Count == 1)
        {
            warTile = selectedTiles[0];
            base.Do(callback); // Command removes the given WarTile from the next war
            // Another command adds a random WarTile 
        }
        else
        {
            callback.Invoke(); 
        }
    }
}
