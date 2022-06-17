using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class BuyBonusWarAction : PlayerAction, ITargetType<Theater>, ITargetType<WarTile>
{
    Theater theater; 
    public Theater target => theater;

    WarTile bonusWarTile; 
    WarTile ITargetType<WarTile>.target => bonusWarTile;

    public override void Do(UnityAction callback)
    {
        bonusWarTile = player.bonusWarTiles.OrderBy(tile => Random.value).First();
        SelectionController.Selection selection = new SelectionController.Selection(Game.NextWarTurn.theaters.ToList<ISelectable>(), 1);

        selection.SetTitle($"{player} Select theater to add {bonusWarTile.tileName}");
        selection.callback = selectedTheaters => Finish(selectedTheaters, callback);
    }

    void Finish(List<ISelectable> selectedTheaters, UnityAction callback)
    {
        theater = (Theater)selectedTheaters[0];
        base.Do(callback);
    }
}
