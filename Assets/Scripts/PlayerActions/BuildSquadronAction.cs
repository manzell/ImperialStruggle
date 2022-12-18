using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class BuildSquadronAction : PlayerAction, PurchaseAction
    {
        public ActionPoint ActionCost => new ActionPoint(ActionPoint.ActionType.Military, ActionPoint.ActionTier.Minor, 4);

        protected override Task Do()
        {
            Commands.Push(new BuildFleetCommand(Player)); 
            return Task.CompletedTask;
        }
    }
}
