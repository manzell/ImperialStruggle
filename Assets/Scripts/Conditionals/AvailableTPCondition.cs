using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class AvailableTPCondition : Conditional
    {
        [SerializeField] int margin = 1; 

        public override bool Test(IPlayerAction context) => RecordsTrack.treatyPoints[context.Player.Faction] >= margin;
    }
}