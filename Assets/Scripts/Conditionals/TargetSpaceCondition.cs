using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class TargetSpaceCondition : Conditional<Space>
    {
        public HashSet<SpaceData> eligibleSpaces = new ();

        protected override bool Test(Space space) => eligibleSpaces.Contains(space.Data); 
    }
}