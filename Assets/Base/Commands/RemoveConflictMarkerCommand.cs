using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveConflictMarkerCommand : Command
{
    public override void Do(BaseAction action)
    {
        if (action is ITargetType<Space> space)
        {
            space.target.conflictMarker = false;
            space.target.updateSpaceEvent.Invoke(); 
        }
    }
}
