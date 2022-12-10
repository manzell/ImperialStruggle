using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class DamagedCondition : Conditional
    {
        public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

        public string ConditionalText => "Fort Damaged";

        public bool Test(GameAction context)
        {
            return true; 
            /*
            if (context is ActionTarget<Fort> fort)
                return fort.target.damaged;
            else
                return false;
            */
        }
    }
}