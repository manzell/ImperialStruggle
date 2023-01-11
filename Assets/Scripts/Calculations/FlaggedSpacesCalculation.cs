using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class FlaggedSpacesCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces;

        protected override int Calc(Player player) => eligibleSpaces.Calculate(player).Count(space => space.Flag == player.Faction); 
    }
    public class ControlledSpacesCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces;
        protected override int Calc(Player player) => eligibleSpaces.Calculate(player).Count(space => space.Control == player.Faction);
    }
}