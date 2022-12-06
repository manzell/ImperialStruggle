using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasNotActedCondition : Conditional
{
    public Conditional.ConditionType ConditionalType => throw new System.NotImplementedException();

    public string ConditionalText => throw new System.NotImplementedException();

    public bool Test(GameAction context)
    {

        return true; 
    }
}
