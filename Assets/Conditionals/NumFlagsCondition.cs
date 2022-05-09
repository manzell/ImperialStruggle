using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

// Does the Target Player control a number of spaces? 
public class NumFlagsCondition : Conditional
{
    [SerializeField] ConditionType conditionType;
    [SerializeField] int targetFlags;
    [SerializeField] Game.Faction targetFaction;
    [SerializeField] List<Space> spaces; 

    // Improve this. Figure out how to do a Chain of Filters on the list of spaces. 
    public override bool Test(BaseAction action)
    {
        int spacesCount = spaces.Where(space => space.flag == targetFaction).Count();

        switch (conditionType)
        {
            case ConditionType.Exactly:
                return spacesCount == targetFlags;
            case ConditionType.MoreThan:
                return spacesCount > targetFlags;
            case ConditionType.FewerThan:
                return spacesCount < targetFlags;
            case ConditionType.NotMoreThan:
                return spacesCount! > targetFlags;
            case ConditionType.NotLessThan:
                return spacesCount! < targetFlags;
            case ConditionType.Not:
                return spacesCount != targetFlags;
            default:
                return true; 
        }
    }
        
}
