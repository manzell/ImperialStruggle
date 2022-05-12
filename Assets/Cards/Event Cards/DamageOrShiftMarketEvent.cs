using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOrShiftMarketEvent : CardEvent
{
    public List<Space> eligibleSpaces; 
    public override void Event()
    {
        Space space = eligibleSpaces[0]; // Note: Figure out how to be listen to the Selector

        if(space is WarTile && space.flag != faction)
        {
            Debug.Log($"Damage {space}"); 
        }
        else if(space is Market && space.flag != faction && (space as Market).marketType == Game.Resource.Cotton)
        {
            Debug.Log($"Shift {space}"); 
        }
    }
}
