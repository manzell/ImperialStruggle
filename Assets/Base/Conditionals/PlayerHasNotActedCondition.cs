using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasNotActedCondition : Conditional
{
    public override bool Test(BaseAction context)
    {
        foreach (BaseAction action in Phase.currentPhase.executedActions)
            if (action is not SelectInvestmentTileAction) return false;

        return true; 
    }
}
