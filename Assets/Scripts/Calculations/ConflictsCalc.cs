using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class ConflictsCalculation : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> spaces;
        protected override int Calc(Player player) => spaces.Calculate(player).Count(space => space.Flag == player.Opponent.Faction);
    }
}
