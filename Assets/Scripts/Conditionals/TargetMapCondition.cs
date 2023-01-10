using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

namespace ImperialStruggle
{
    public class TargetMapCondition : Conditional
    {
        [SerializeField] public HashSet<Map> eligibleMaps;

        public TargetMapCondition(Map map) => eligibleMaps = new() { map };
        public TargetMapCondition(IEnumerable<Map> maps) => eligibleMaps = new(maps); 

        public override bool Test(IPlayerAction context) => context is TargetSpaceAction<Space> action ?
                eligibleMaps.Contains(action.Space.map) : false; 
    }
}
