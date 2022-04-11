using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class NumFlagsCondition : Conditional<Game.Faction>
{
    [SerializeField] ConditionType conditionType;
    [SerializeField] int targetFlags;
    [SerializeField] Game.Faction targetFaction;
    [SerializeField] List<Space> spaces; 

    public override bool Test(Game.Faction faction)
    {
        int spacesCount = spaces.Where(space => space.flag == targetFaction).Count();

        switch(conditionType)
        {
            case ConditionType.Exactly:
                return spacesCount == targetFlags;
            case ConditionType.MoreThan:
                return spacesCount > targetFlags;
            case ConditionType.FewerThan:
                return spacesCount < targetFlags;
            case ConditionType.NotMoreThan:
                return spacesCount !> targetFlags;
            case ConditionType.NotLessThan:
                return spacesCount !< targetFlags;
            case ConditionType.Not:
                return spacesCount != targetFlags;
        }

        return false; 
    }
        
}
