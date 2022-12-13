using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    // Does the Target Player control a number of spaces? 
    public class NumFlagsCondition : Conditional
    {
        [SerializeField] int targetFlags;
        [SerializeField] Faction targetFaction;
        [SerializeField] List<Space> spaces;

        public override bool Test(GameAction action)
        {
            int spacesCount = spaces.Where(space => space.Flag == targetFaction).Count();

            switch (ConditionalType)
            {
                case Conditional.ConditionType.Exactly:
                    return spacesCount == targetFlags;
                case Conditional.ConditionType.MoreThan:
                    return spacesCount > targetFlags;
                case Conditional.ConditionType.FewerThan:
                    return spacesCount < targetFlags;
                case Conditional.ConditionType.NotMoreThan:
                    return spacesCount <= targetFlags;
                case Conditional.ConditionType.NotLessThan:
                    return spacesCount >= targetFlags;
                default:
                    return true;
            }
        }
    }
}