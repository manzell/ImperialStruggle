using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class SquadronsCalc : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> spaces;
        protected override int Calc(Player player) => spaces.Calculate(player).OfType<NavalSpace>().Count(space => space.Flag == player.Faction);
    }
}
