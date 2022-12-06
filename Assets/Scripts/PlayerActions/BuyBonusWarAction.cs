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

        SelectionController.Selection selection = new SelectionController.Selection(Game.NextWarTurn.theaters.ToList<ISelectable>(), 1);

        selection.SetTitle($"{actingPlayer} Select theater to add {bonusWarTile.tileName}");
        selection.callback = selectedTheaters => Finish(selectedTheaters);
    }

    void Finish(List<ISelectable> selectedTheaters)
    {
        commands.Push(new AddWarTileToTheaterCommand(bonusWarTile, (Theater)selectedTheaters.First())); 
    }
}
