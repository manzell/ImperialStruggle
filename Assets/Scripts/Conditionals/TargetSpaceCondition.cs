using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class TargetSpaceCondition : Conditional
    {
        public HashSet<SpaceData> eligibleSpaces = new ();

        public override bool Test(IPlayerAction context) => context is TargetSpaceAction<Space> action ? eligibleSpaces.Contains(action.Space.Data) : false; 
    }
}