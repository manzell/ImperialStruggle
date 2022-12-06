using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableTPCondition : Conditional
{
    public Conditional.ConditionType ConditionalType { get; private set; }

    public string ConditionalText => "Available Treaty Points"; 

    public bool Test(GameAction context)
    {
        if (context is ActionTarget<Player> playerTargetAction)
            return RecordsTrack.treatyPoints[playerTargetAction.target.faction] > 0;
        else if (context is IPlayerAction p)
            return RecordsTrack.treatyPoints[p.player.faction] > 0;
        else return false; 
    }
}
