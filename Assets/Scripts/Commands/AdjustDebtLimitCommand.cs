using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustDebtLimitCommand : Command
{
    Faction faction;
    int amount; 

    public AdjustDebtLimitCommand(Faction faction, int amount)
    {
        this.faction = faction;
        this.amount = amount; 
    }

    public override void Do(GameAction action)
    {
        // There is no side-effect of reducing debt limit below current debt. We just live with it! 
        if (action is AdjustDebtLimitAction debtAction)
        {
            int debtLimit = Mathf.Max(0, RecordsTrack.debtLimit[faction] + debtAction.amount);
            RecordsTrack.debtLimit[faction] = debtLimit;

            RecordsTrack.adjustDebtLimitEvent.Invoke(); 

            Game.Log($"Adjusting {faction} Debt Limit by {debtAction.amount} (to {debtLimit})");            
        }
    }
}
