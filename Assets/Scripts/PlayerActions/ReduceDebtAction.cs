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

        public override bool Can()
        {
            return base.Can() && RecordsTrack.currentDebt[Player.Faction] > 0 && 
                !Phase.CurrentPhase.ExecutedActions.OfType<PurchaseAction>().Any();
        }

        protected override Task Do()
        {
            Commands.Push(new AdjustDebtCommand(Player.Faction, debtAdjustment));
            return Task.CompletedTask;
        }
    }
}