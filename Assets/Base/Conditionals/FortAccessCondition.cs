using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortAccessCondition : Conditional
{
    public override bool Test(BaseAction context)
    {
        if (context is PlayerAction playerAction && context is ITargetType<Fort> fort)
        {
            if (fort.target.flag == playerAction.player.faction)
                return true; 

            foreach (Space space in fort.target.adjacentSpaces)
                if ((space is Market || space is Territory || space is NavalSpace) && space.flag == playerAction.player.faction)
                    return true;

            return false;
        }
        return true;
    }
}
