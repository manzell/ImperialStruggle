using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [System.Serializable]
    public class SpaceControlCondition : Conditional
    {
        enum SpaceConditionType { Friendly, Opposed, Neutral, Nonfriendly, Nonopposed }
        [SerializeField] Space space; 
        [SerializeField] SpaceConditionType spaceConditionType;

        public override bool Test(IPlayerAction action)
        {
            switch (spaceConditionType)
            {
                case SpaceConditionType.Friendly:
                    return space.Flag == action.Player.Faction;
                case SpaceConditionType.Nonfriendly:
                    return space.Flag != action.Player.Faction;
                case SpaceConditionType.Opposed:
                    return space.Flag != action.Player.Faction && space.Flag != null;
                case SpaceConditionType.Nonopposed:
                    return space.Flag == action.Player.Faction || space.Flag == null;
                case SpaceConditionType.Neutral:
                    return space.Flag == Game.Neutral;
                default:
                    return false; 
            }
        }
    }
}