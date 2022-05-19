using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableTPCondition : Conditional
{
    public override bool Test(BaseAction context)
    {
        if (context is PlayerAction p)
            return RecordsTrack.treatyPoints[p.player.faction] > 0;
        else if(context is ITargetType<Player> _p)
            return RecordsTrack.treatyPoints[_p.target.faction] > 0;
        return true; 
    }
}
