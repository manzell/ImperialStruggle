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
            space.SetConflictMarker(false);
            space.updateSpaceEvent.Invoke();
        }
    }
}