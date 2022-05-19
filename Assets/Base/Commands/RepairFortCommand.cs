using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFortCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is PlayerAction playerAction && action is ITargetType<Fort> fort)
        {
            fort.target.damaged = false;
            Debug.Log($"{playerAction.player} repaired {fort.target.name}"); 
        }
    }
}