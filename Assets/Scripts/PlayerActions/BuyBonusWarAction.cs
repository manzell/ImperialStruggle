using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace ImperialStruggle
{
    public class BuyBonusWarAction : PlayerAction
    {
        WarTile bonusWarTile;

        protected override void Do()
        {
            bonusWarTile = player.bonusWarTiles.OrderBy(tile => Random.value).First();

            Selection<Theater> selection = new(player, Game.NextWarTurn.theaters, Finish);
            //selection.SetTitle($"{actingPlayer} Select theater to add {bonusWarTile.tileName}");
        }

        void Finish(Selection<Theater> selection)
        {
            Commands.Push(new AddWarTileToTheaterCommand(bonusWarTile, selection.First()));
        }
    }
}