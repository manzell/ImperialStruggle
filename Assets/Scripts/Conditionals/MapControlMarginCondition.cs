using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class MapControlMarginCondition : Conditional
    {
        [SerializeField] int requiredMargin = 2;

        public override bool Test(IPlayerAction action) => true;
            //action is ITargetMap mapAction ?
              //  Mathf.Abs(mapAction.map.mapScore[Game.Britain] - mapAction.map.mapScore[Game.France]) >= requiredMargin : true;
    }
}