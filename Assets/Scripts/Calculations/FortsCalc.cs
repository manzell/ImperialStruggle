using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class FortsCalc : Calculation<int>
    {
        [SerializeField] Calculation<HashSet<Space>> spaces;
        protected override int Calc(Player player) => spaces.Calculate(player).OfType<Fort>().Count(space => space.Flag == player.Faction);
    }
}
