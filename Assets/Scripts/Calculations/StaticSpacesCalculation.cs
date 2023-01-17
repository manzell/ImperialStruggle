using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class StaticSpacesCalculation : Calculation<HashSet<Space>>
    {
        [SerializeField] HashSet<SpaceData> spaces;
        protected override HashSet<Space> Calc(IAction context) => new(spaces.Select(data => Game.SpaceLookup[data]));
    }
}
