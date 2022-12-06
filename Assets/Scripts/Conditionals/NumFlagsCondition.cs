using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

// Does the Target Player control a number of spaces? 
public class NumFlagsCondition : Conditional
{
    [SerializeField] int targetFlags;
    [SerializeField] Faction targetFaction;
    [SerializeField] List<Space> spaces;

    public Conditional.ConditionType ConditionalType => throw new System.NotImplementedException();

    public string ConditionalText => throw new System.NotImplementedException();

    public bool Test(GameAction action)
    {
        int spacesCount = spaces.Where(space => space.flag == targetFaction).Count();

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
