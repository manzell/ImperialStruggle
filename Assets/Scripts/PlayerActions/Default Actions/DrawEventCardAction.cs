using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Splines.SplineInstantiate;

namespace ImperialStruggle
{
    public class DrawEventCardAction : PlayerAction, _PurchaseAction
    {
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Diplomacy, 3);

        public override bool Can(Player player) => Game.EventDeck.Count > 0 && base.Can(player); 

        protected override Task Do(IAction context)
        {
            Commands.Push(new DealCardCommand(Player));
            return Task.CompletedTask;
        }
    }
}
