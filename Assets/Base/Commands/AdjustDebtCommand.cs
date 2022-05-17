using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtCommand : Command
{
    public override void Do(BaseAction action)
    {
        if (action is IAdjustDebt adjustDebtAction && adjustDebtAction.debt != 0)
        {
            RecordsTrack.currentDebt[adjustDebtAction.faction] += adjustDebtAction.debt;

            RecordsTrack.adjustDebtEvent.Invoke(); 
            // Check to see if Debt > Debt Limit
        }
    }
}
