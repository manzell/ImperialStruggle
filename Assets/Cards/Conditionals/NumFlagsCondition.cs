using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class NumFlagsCondition : Conditional
{
    [SerializeField] int targetFlags;
    [SerializeField] Game.Faction targetFaction;
    [SerializeField] List<Space> spaces; 

    public override bool Test(Game.Faction faction) =>
        spaces.Where(space => space.flag == targetFaction).Count() == targetFlags; 
}
