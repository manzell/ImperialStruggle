using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class BuyBonusWarAction : PlayerAction, PurchaseAction
    {
        WarTile bonusWarTile;

        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor, 2);

        public override bool Can() => base.Can() && Phase.CurrentPhase.ExecutedActions.Count(action => action is BuyBonusWarAction) >= 2; 

        protected override async Task Do()
        {
            bonusWarTile = Player.BonusWarTiles.OrderBy(tile => Random.value).First();

            Selection<Theater> selection = new(Player, Game.NextWarTurn.theaters, Finish);
            selection.SetTitle($"{Player} Select theater to add {bonusWarTile.Name}");

            await selection.Completion;
        }

        void Finish(IEnumerable<Theater> theater) => Commands.Push(new AddWarTileToTheaterCommand(bonusWarTile, theater.First()));
    }
}