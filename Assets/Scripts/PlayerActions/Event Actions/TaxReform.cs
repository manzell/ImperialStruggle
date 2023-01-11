using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class TaxReform_Base : PlayerAction
    {
        [SerializeField] int debtReductionAmount = 2; 
        protected override Task Do()
        {
            int debtReduction = Mathf.Min(debtReductionAmount, RecordsTrack.currentDebt[Player.Faction]);
            int EPaward = debtReductionAmount - debtReduction;

            if(debtReduction > 0)
                Commands.Push(new AdjustDebtCommand(Player.Faction, -debtReduction));

            if (EPaward > 0)
                Commands.Push(new AddAPCommand(Player, new ActionPoint(ActionPoint.ActionTier.Major, ActionPoint.ActionType.Finance, EPaward))); 

            return Task.CompletedTask; 
        }
    }

    public class TaxReform_Bonus : PlayerAction
    {
        [SerializeField] int debtReductionAmount = 1;
        protected override Task Do()
        {
            int debtReduction = Mathf.Min(debtReductionAmount, RecordsTrack.currentDebt[Player.Faction]);
            int EPaward = debtReductionAmount - debtReduction;

            if (debtReduction > 0)
                Commands.Push(new AdjustDebtCommand(Player.Faction, -debtReduction));

            if (EPaward > 0)
                Commands.Push(new AddAPCommand(Player, new ActionPoint(ActionPoint.ActionTier.Major, ActionPoint.ActionType.Finance, EPaward)));

            return Task.CompletedTask;
        }
    }
}
