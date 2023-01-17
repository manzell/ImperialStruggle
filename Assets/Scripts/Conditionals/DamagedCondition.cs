using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class DamagedCondition : Conditional<Fort>
    {
        protected override bool Test(Fort fort) => fort.damaged; 
    }
}