using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class BuildSquadronAction : PlayerAction, _PurchaseAction
    {
        [field: SerializeField] public ActionPoint ActionCost { get; private set; } = new ActionPoint(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 4);

        protected override Task Do(IAction context)
        {
            Commands.Push(new BuildFleetCommand(Player)); 
            return Task.CompletedTask;
        }
    }
}
