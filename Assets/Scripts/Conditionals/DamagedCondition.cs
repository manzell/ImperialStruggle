using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class DamagedCondition : Conditional
    {
        public override bool Test(GameAction context)
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