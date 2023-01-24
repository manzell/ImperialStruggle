using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class MapControlMarginCondition : Conditional<Faction>
    {
        [SerializeField] Calculation<Map> mapCalc; 

        protected override bool Test(Faction faction)
        {
            Map map = mapCalc.Calculate();
            return Mathf.Abs(map.mapScore[faction] - map.mapScore[faction.Opposition()]) >= map.awardTile.Margin; 
        }
    }
}