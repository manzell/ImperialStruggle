using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks; 

namespace ImperialStruggle
{
    public class BuyBonusWarAction : PlayerAction, _PurchaseAction, IExhaustable
    {
        WarTile bonusWarTile;

        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 2);

        public bool exhausted { get; set; }

        public override bool Can(Player player) => base.Can(player);// && Phase.CurrentPhase.ExecutedActions.Count(action => action is BuyBonusWarAction) >= 2; 

        protected override async Task Do(IAction context)
        {
            bonusWarTile = Player.BonusWarTiles.OrderBy(tile => Random.value).First();

            Selection<Theater> selection = new(Player, Game.NextWarTurn.theaters, Finish);
            selection.SetTitle($"{Player} Select theater to add {bonusWarTile.Name}");

            await selection.Completion;
        }

        void Finish(IEnumerable<Theater> theater) => Commands.Push(new AddWarTileToTheaterCommand(bonusWarTile, theater.First()));
    }
}