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

    public override void Do(UnityAction callback)
    {   
        List<ISelectable> warTiles = new List<ISelectable>();
        nextWar = Game.NextWarTurn;
        
        nextWar.theaters.ForEach(theater => warTiles.AddRange(theater.warTiles[player.faction].Where(tile => tile.warTileSet == WarTile.WarTileSet.Basic).ToList<ISelectable>()));

        if(warTiles.Count > 0)
        {
            SelectionController.Selection selection = FindObjectOfType<SelectionController>().Select(warTiles, 1);
            selection.SetTitle($"Select a {player.faction} Basic War Tile to replace");
            selection.callback = selectedTiles => Finish(selectedTiles, callback);
        }
        else
        {
            callback.Invoke(); 
        }
    }

    void Finish(List<ISelectable> selectedTiles, UnityAction callback)
    {
        if(selectedTiles.Count == 1)
        {
            warTile = (WarTile)selectedTiles[0];
            base.Do(callback); 
        }
        else
        {
            callback.Invoke(); 
        }
    }
}
