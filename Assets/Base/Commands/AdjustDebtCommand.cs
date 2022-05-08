using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtCommand : Command
{
    public override void Do(BaseAction action)
    {
        if (action is IAdjustDebt adjustDebtAction && adjustDebtAction.debt != 0)
        {
            GameObject.FindObjectOfType<RecordsTrack>().currentDebt[adjustDebtAction.faction] += adjustDebtAction.debt;
            // Check to see if Debt > Debt Limit
        }
    }
}
