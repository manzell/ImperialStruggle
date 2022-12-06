using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlCondition : Conditional
{
    public Conditional.ConditionType ConditionalType => Conditional.ConditionType.Exactly;

    public string ConditionalText => "Map Control"; 

    [SerializeField] Map map; 
    public bool Test(GameAction context)
    {
        if (context is PlayerAction playerAction)
            return map.controllingFaction == playerAction.actingPlayer.faction;
        return false; 
    }
}
