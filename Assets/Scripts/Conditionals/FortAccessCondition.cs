using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class FortAccessCondition : Conditional
{
    public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

    public string ConditionalText => "Fort Access";

    public bool Test(GameAction context)
    {
        if (context is PlayerAction playerAction && context is ActionTarget<Fort> fort)
        {
            if (fort.target.flag == playerAction.actingPlayer.faction)
                return true; 

            foreach (Space space in fort.target.adjacentSpaces)
                if ((space is Market || space is Territory || space is NavalSpace) && space.flag == playerAction.actingPlayer.faction)
                    return true;
        }

        return false;        
    }
}
*/