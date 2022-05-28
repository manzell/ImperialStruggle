using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedCondition : Conditional
{
    public override bool Test(BaseAction context)
    {
        if (context is ITargetType<Fort> fort)
            return fort.target.damaged;
        else 
            return false; 
    }
}
