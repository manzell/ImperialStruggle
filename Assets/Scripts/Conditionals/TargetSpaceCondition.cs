using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class TargetSpaceCondition : Conditional
    {
        public List<Space> eligibleSpaces = new List<Space>();

        public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

        public string ConditionalText => "Target Space??";

        public bool Test(GameAction context)
        {
            /*
            if (context is ActionTarget<Space> space)
                return eligibleSpaces.Contains(space.target);

            if (context is ActionTarget<List<Space>> spaceList)
                return spaceList.target.All(space => eligibleSpaces.Contains(space));
            */
            return false;
        }
    }
}