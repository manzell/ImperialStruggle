using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceControlCondition : Conditional
{
    enum SpaceConditionType { Friendly, Opposed, Neutral, Nonfriendly, Nonopposed }
    [SerializeField] SpaceConditionType spaceConditionType;

    public Conditional.ConditionType ConditionalType => throw new System.NotImplementedException();

    public string ConditionalText => throw new System.NotImplementedException();

    public bool Test(GameAction action)
    {        
        if(action is ActionTarget<Space> spaceAction && action is PlayerAction playerAction)
        {
            switch (spaceConditionType)
            {
                case SpaceConditionType.Friendly:
                    return spaceAction.target.flag == playerAction.actingPlayer.faction; 
                case SpaceConditionType.Nonfriendly:
                    return spaceAction.target.flag != playerAction.actingPlayer.faction;
                case SpaceConditionType.Opposed:
                    return spaceAction.target.flag != playerAction.actingPlayer.faction &&
                        spaceAction.target.flag != null; 
                case SpaceConditionType.Nonopposed:
                    return spaceAction.target.flag == playerAction.actingPlayer.faction ||
                        spaceAction.target.flag == null;
                case SpaceConditionType.Neutral:
                    return spaceAction.target.flag == null;
            }
        }
        return false; 
    }
}
