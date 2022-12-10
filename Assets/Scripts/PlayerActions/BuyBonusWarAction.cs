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
            bonusWarTile = actingPlayer.bonusWarTiles.OrderBy(tile => Random.value).First();

            Selection<Theater> selection = new(actingPlayer, Game.NextWarTurn.theaters, Finish);
            //selection.SetTitle($"{actingPlayer} Select theater to add {bonusWarTile.tileName}");
        }

        void Finish(Selection<Theater> selection)
        {
            commands.Add(new AddWarTileToTheaterCommand(bonusWarTile, selection.First()));
        }
    }
}