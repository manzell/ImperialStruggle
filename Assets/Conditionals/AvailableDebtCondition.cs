using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableDebtCondition : Conditional
{
    enum LimitConditionType { AtLeast, Exactly }
    [SerializeField] LimitConditionType limitConditionType;
    [SerializeField] int margin = 1;
    [SerializeField] Game.Faction targetFaction;

    // Does the Faction of the given Player have at least margin available debt? 
    public override bool Test(BaseAction action)
    {
        int availableDebt = FindObjectOfType<RecordsTrack>().availableDebt[targetFaction]; 

        switch(limitConditionType)
        {
            case LimitConditionType.Exactly:
                return availableDebt == margin;
            case LimitConditionType.AtLeast:
                return availableDebt >= margin;
            default:
                return true; 
        }                
    }
}