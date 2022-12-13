using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [System.Serializable]
    public class SpaceControlCondition : Conditional
    {
        enum SpaceConditionType { Friendly, Opposed, Neutral, Nonfriendly, Nonopposed }
        [SerializeField] SpaceConditionType spaceConditionType;

        public override bool Test(GameAction action)
        {    
            /*
            if(action is ActionTarget<Space> spaceAction && action is PlayerAction playerAction)
            {
                switch (spaceConditionType)
                {
                    case SpaceConditionType.Friendly:
                        return spaceAction.target.Flag == playerAction.actingPlayer.faction; 
                    case SpaceConditionType.Nonfriendly:
                        return spaceAction.target.Flag != playerAction.actingPlayer.faction;
                    case SpaceConditionType.Opposed:
                        return spaceAction.target.Flag != playerAction.actingPlayer.faction &&
                            spaceAction.target.Flag != null; 
                    case SpaceConditionType.Nonopposed:
                        return spaceAction.target.Flag == playerAction.actingPlayer.faction ||
                            spaceAction.target.Flag == null;
                    case SpaceConditionType.Neutral:
                        return spaceAction.target.Flag == null;
                }
            }
            */
            return false; 
        }
    }
}