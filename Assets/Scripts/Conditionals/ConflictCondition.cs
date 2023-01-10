using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ConflictCondition : Conditional
    {
        public override bool Test(IPlayerAction context)
        {
            Debug.LogWarning("Conflict Condition doesn't know what space to evaluate"); 
            return true;
            /*
            if (context is ActionTarget<Space> space)
                return space.target.conflictMarker;
            else
                return false;
            */
        }
    }
}