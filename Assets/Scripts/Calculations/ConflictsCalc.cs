using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class ConflictsCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> spaces;
        protected override int Calc(IAction context) => spaces.Calculate(context).Count(space => space.Flag == (context as PlayerAction)?.Player.Opponent.Faction);
    }
}
