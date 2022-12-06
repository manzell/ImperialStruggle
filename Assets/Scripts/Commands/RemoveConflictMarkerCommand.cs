using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveConflictMarkerCommand : Command
{
    Space space; 
    public RemoveConflictMarkerCommand(Space space)
    {
        this.space = space;
    }

    public override void Do(GameAction action)
    {
        space.SetConflictMarker(false);
        space.updateSpaceEvent.Invoke(); 
    }
}
