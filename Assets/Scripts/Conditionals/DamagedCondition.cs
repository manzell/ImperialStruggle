using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedCondition : Conditional
{
    public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

    public string ConditionalText => "Fort Damaged";

    public bool Test(GameAction context)
    {
        if (context is ActionTarget<Fort> fort)
            return fort.target.damaged;
        else 
            return false; 
    }
}
