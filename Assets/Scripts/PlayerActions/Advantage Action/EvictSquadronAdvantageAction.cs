using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class EvictSquadronAdvantageAction : PlayerAction, _PurchaseAction
    {
        [field: SerializeField] public ActionPoint ActionCost { get; private set; } = new(ActionPoint.ActionTier.Minor, ActionPoint.ActionType.Military, 1);

        protected override async Task Do(IAction context)
        {
            Selection<Squadron> selection = new(Player, Player.Opponent.Squadrons.Where(squadron => squadron.space != null));
            await selection.Completion;

            if (selection.Count() > 0)
                Commands.Push(new DeploySquadronCommand(selection.First(), null)); 
        }
    }
}
