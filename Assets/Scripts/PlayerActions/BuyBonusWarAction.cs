using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class BuyBonusWarAction : PlayerAction
{
    WarTile bonusWarTile; 

    protected override void Do()
    {
        bonusWarTile = actingPlayer.bonusWarTiles.OrderBy(tile => Random.value).First();

        SelectionController<Theater>.Selection selection = new (Game.NextWarTurn.theaters, Finish);
        selection.SetTitle($"{actingPlayer} Select theater to add {bonusWarTile.tileName}");
    }

    void Finish(Theater selectedTheater)
    {
        commands.Add(new AddWarTileToTheaterCommand(bonusWarTile, selectedTheater)); 
    }
}
