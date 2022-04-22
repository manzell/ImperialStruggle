using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCondition : Conditional
{
    [SerializeField] Calculation<List<Space>> spaces;
    [SerializeField] List<Game.Faction> eligibleFlags, ineligibleFlags; 

    public override bool Test(Object _space) =>
        eligibleFlags.Count > 0 && _space is Space ? 
            eligibleFlags.Contains((_space as Space).flag) || !ineligibleFlags.Contains((_space as Space).flag) : true; 
}
