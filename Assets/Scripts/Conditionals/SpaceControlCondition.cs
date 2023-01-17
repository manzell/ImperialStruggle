using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [System.Serializable]
    public class SpaceControlCondition : Conditional<Space>
    {
        enum SpaceConditionType { Friendly, Opposed, Neutral, Nonfriendly, Nonopposed }
        [SerializeField] Faction faction; 
        [SerializeField] SpaceConditionType spaceConditionType;

        protected override bool Test(Space space)
        {
            switch (spaceConditionType)
            {
                case SpaceConditionType.Friendly:
                    return space.Flag == faction;
                case SpaceConditionType.Nonfriendly:
                    return space.Flag != faction;
                case SpaceConditionType.Opposed:
                    return space.Flag != faction && space.Flag != null;
                case SpaceConditionType.Nonopposed:
                    return space.Flag == faction || space.Flag == null;
                case SpaceConditionType.Neutral:
                    return space.Flag == Game.Neutral;
                default:
                    return false; 
            }
        }
    }
}