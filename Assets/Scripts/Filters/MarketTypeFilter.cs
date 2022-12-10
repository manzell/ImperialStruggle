using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class MarketTypeFilter : Filter<Space>
    {
        [SerializeField] HashSet<Resource> marketTypes;

        public override IEnumerable<Space> Apply(IEnumerable<Space> t) =>
            t.Where(space => space is Market && marketTypes.Contains((space as Market).Resource));
    }
}