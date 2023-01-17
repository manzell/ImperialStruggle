using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AvailableTPCondition : Conditional<Faction>
    {
        [SerializeField] int margin = 1; 

        protected override bool Test(Faction faction) => RecordsTrack.treatyPoints[faction] >= margin;
    }
}