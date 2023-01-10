using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Splines.SplineInstantiate;

namespace ImperialStruggle
{
    public class AddConflictMarkerCommand : Command
    {
        Space space;
        public AddConflictMarkerCommand(Space space)
        {
            this.space = space;
        }

        public override void Do(GameAction action)
        {
            //space.SetConflictMarker(true);
            space.updateSpaceEvent.Invoke();
        }
    }
}
