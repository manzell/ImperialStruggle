using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFortCommand : Command
{
    public override void Do(GameAction action)
    {
        if(action is PlayerAction playerAction && action is ActionTarget<Fort> fort)
        {
            fort.target.damaged = false;
            Debug.Log($"{playerAction.actingPlayer} repaired {fort.target.name}"); 
        }
    }
}