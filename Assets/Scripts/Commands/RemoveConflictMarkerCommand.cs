using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class RemoveConflictMarkerCommand : Command
    {
        Space space;
        public RemoveConflictMarkerCommand(Space space)
        {
            this.space = space;
        }

        public override void Do(GameAction action)
        {
            if (space.ConflictMarkers.Count > 0)
                space.ConflictMarkers.RemoveAt(0); 
            space.updateSpaceEvent.Invoke();
        }
    }
}