using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class FlaggedSpacesCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces;

        protected override int Calc(IAction context) => eligibleSpaces.Calculate(context).Count(space => space.Flag == (context as PlayerAction)?.Player.Faction); 
    }
    public class ControlledSpacesCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> eligibleSpaces;
        protected override int Calc(IAction context) => eligibleSpaces.Calculate(context).Count(space => space.Control == (context as PlayerAction)?.Player.Faction);
    }
}