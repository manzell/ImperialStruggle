using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class ConflictCondition : Conditional
    {
        public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

        public string ConditionalText => "Space in Conflict";

        public bool Test(GameAction context)
        {
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