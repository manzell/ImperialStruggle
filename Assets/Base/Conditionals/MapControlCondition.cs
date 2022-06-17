using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlCondition : Conditional
{
    [SerializeField] Map map; 
    public override bool Test(BaseAction context)
    {
        if (context is PlayerAction playerAction)
            return map.controllingFaction == playerAction.player.faction;
        return false; 
    }
}
