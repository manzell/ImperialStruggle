using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtLimitCommand : Command
{
    public override void Do(BaseAction action)
    {
        // There is no side-effect of reducing debt limit below current debt. We just live with it! 
        if (action is AdjustDebtLimitAction debtAction)
        {
            RecordsTrack records = GameObject.FindObjectOfType<RecordsTrack>();

            int debtLimit = Mathf.Max(0, records.debtLimit[debtAction.faction] + debtAction.amount);

            Game.Log($"Adjusting {debtAction.faction} Debt Limit by {debtAction.amount} (to {debtLimit})"); 
            records.debtLimit[debtAction.faction] = debtLimit;
        }
    }
}
