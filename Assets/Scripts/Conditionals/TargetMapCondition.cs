using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class TargetMapCondition : Conditional<IAction>
    {
        [SerializeField] List<Map> eligibleMaps; 
        protected override bool Test(IAction context) => context is TargetSpaceAction<Space> targetSpace && eligibleMaps.Contains(targetSpace.Space.map); 
    }
}
