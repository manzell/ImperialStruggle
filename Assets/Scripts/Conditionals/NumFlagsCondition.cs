using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    // Does the Target Player control a number of spaces? 
    public class NumFlagsCondition : Conditional<Faction>
    {
        enum ConditionType { Exactly, MoreThan, FewerThan, NotMoreThan, NotLessThan }
        [SerializeField] int targetFlags;
        [SerializeField] Calculation<List<Space>> spacesCalc;
        [SerializeField] ConditionType conditionType; 

        protected override bool Test(Faction faction)
        {
            int spacesCount = spacesCalc.Calculate().Where(space => space.Flag == faction).Count();

            switch (conditionType)
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