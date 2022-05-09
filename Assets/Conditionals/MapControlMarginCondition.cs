using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MapControlMarginCondition : Conditional
{
    [SerializeField] int requiredMargin = 2; 

    public override bool Test(BaseAction action)
    {
        if(action is ITargetMap mapAction)
            return Mathf.Abs(mapAction.map.mapScore[Game.Faction.Britain] - mapAction.map.mapScore[Game.Faction.France]) >= requiredMargin;
        else
            return true; 
    }
}
