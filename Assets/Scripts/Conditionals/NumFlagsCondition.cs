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

        public override bool Test(IPlayerAction action)
        {
            int spacesCount = spaces.Where(space => space.Flag == (targetFaction ?? action.Player.Faction)).Count();

            switch (ConditionalType)
            {
                case ConditionType.Exactly:
                    return spacesCount == targetFlags;
                case ConditionType.MoreThan:
                    return spacesCount > targetFlags;
                case ConditionType.FewerThan:
                    return spacesCount < targetFlags;
                case ConditionType.NotMoreThan:
                    return spacesCount <= targetFlags;
                case ConditionType.NotLessThan:
                    return spacesCount >= targetFlags;
                default:
                    return true;
            }
        }
    }
}