using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class RegionFilter : Conditional<Space>
    {
        [SerializeField] Region region;

        protected override bool Test(Space space) => space.Data.Region == region;  
    }
}
