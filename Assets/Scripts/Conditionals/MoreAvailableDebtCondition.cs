using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MoreAvailableDebtCondition : Conditional
    {
        [SerializeField] int margin = 1;

        public override bool Test(IPlayerAction context) => margin == 0 ? 
            RecordsTrack.availableDebt[context.Player.Faction] == RecordsTrack.availableDebt[context.Player.Opponent.Faction] :
            RecordsTrack.availableDebt[context.Player.Faction] - RecordsTrack.availableDebt[context.Player.Opponent.Faction] >= margin;
    }
}