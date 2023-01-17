using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AdjustDebtAction : PlayerAction
    {
        [SerializeField] int debtAdjustment;

        public override bool Can(Player player)
        {
            Debug.Log("Warning - Does not check for purchases in Multiple Regions/Maps");
            return base.Can(player) && RecordsTrack.currentDebt[player.Faction] > 0; //&& 
                //!Phase.CurrentPhase.ExecutedActions.OfType<_PurchaseAction>().Any();
        }

        protected override Task Do(IAction context)
        {
            Commands.Push(new AdjustDebtCommand(Player.Faction, debtAdjustment));
            return Task.CompletedTask;
        }
    }
}