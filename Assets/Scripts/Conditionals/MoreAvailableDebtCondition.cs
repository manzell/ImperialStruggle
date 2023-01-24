using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MoreAvailableDebtCondition : Conditional<PlayerAction>
    {
        [SerializeField] int margin = 1;

        protected override bool Test(PlayerAction action) => margin == 0 ?
                RecordsTrack.availableDebt[action.Player.Faction] == RecordsTrack.availableDebt[action.Player.Opponent.Faction] :
                RecordsTrack.availableDebt[action.Player.Faction] - RecordsTrack.availableDebt[action.Player.Opponent.Faction] >= margin;
    }
}