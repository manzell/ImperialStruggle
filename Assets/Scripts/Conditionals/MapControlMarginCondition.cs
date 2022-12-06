using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MapControlMarginCondition : Conditional
{
    [SerializeField] int requiredMargin = 2;

    public Conditional.ConditionType ConditionalType => Conditional.ConditionType.MoreThan;

    public string ConditionalText => throw new System.NotImplementedException();

    public bool Test(GameAction action) => action is ITargetMap mapAction ?
            Mathf.Abs(mapAction.map.mapScore[Game.Britain] - mapAction.map.mapScore[Game.France]) >= requiredMargin : true; 
}
