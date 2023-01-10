using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class SpaceOnMapCalculation : Calculation<IEnumerable<Space>>
    {
        [SerializeField] List<Map> eligibleMaps = new List<Map>();

        public override IEnumerable<Space> Calculate()
        {
            List<Space> spaces = new List<Space>();

            foreach (Map map in eligibleMaps)
                spaces.AddRange(map.spaces);

            return spaces;
        }
    }
}