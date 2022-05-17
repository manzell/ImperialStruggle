using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictCondition : Conditional
{
    public override bool Test(BaseAction context)
    {
        if(context is ITargetType<Space> space)
            return space.target.conflictMarker;

        return true; 
    }
}
