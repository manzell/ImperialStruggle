using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ShiftSpaceCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<Game.Faction> faction && action is ITargetType<Space> space)
        {
            space.target.flag = space.target.flag == Game.Faction.Neutral ? faction.target : Game.Faction.Neutral;
            Debug.Log($"{space.target.name} flag set to {space.target.flag}");

            space.target.updateSpaceEvent.Invoke(); 
        }
    }
}
