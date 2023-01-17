using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class SpacesCalc : Calculation<IEnumerable<ISelectable>>
    {
        [SerializeField] List<Conditional<Space>> SpaceFilters = new();

        protected override IEnumerable<ISelectable> Calc(IAction context) => Game.Spaces
            .Where(space => SpaceFilters.All(filter => filter.Check(space)));
    }
}