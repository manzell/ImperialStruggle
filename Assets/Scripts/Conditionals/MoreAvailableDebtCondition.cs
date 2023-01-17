using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MoreAvailableDebtCondition : Conditional<Faction>
    {
        [SerializeField] int margin = 1;

        protected override bool Test(Faction faction) => margin == 0 ?
                RecordsTrack.availableDebt[faction] == RecordsTrack.availableDebt[faction.Opposition()] :
                RecordsTrack.availableDebt[faction] - RecordsTrack.availableDebt[faction.Opposition()] >= margin;
    }
}