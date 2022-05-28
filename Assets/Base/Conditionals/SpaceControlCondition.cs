using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceControlCondition : Conditional
{
    enum SpaceConditionType { Friendly, Opposed, Neutral, Nonfriendly, Nonopposed }
    [SerializeField] SpaceConditionType spaceConditionType;

    public override bool Test(BaseAction action)
    {        
        if(action is ITargetType<Space> spaceAction && action is PlayerAction playerAction)
        {
            switch (spaceConditionType)
            {
                case SpaceConditionType.Friendly:
                    return spaceAction.target.flag == playerAction.player.faction; 
                case SpaceConditionType.Nonfriendly:
                    return spaceAction.target.flag != playerAction.player.faction;
                case SpaceConditionType.Opposed:
                    return spaceAction.target.flag != playerAction.player.faction &&
                        spaceAction.target.flag != Game.Faction.Neutral; 
                case SpaceConditionType.Nonopposed:
                    return spaceAction.target.flag == playerAction.player.faction ||
                        spaceAction.target.flag == Game.Faction.Neutral;
                case SpaceConditionType.Neutral:
                    return spaceAction.target.flag == Game.Faction.Neutral;
            }
        }
        return false; 
    }
}
