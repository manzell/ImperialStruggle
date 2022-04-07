using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ControlBonus : Conditional
{
    public enum ConditionalMode { Any, All, More }
    [SerializeField] ConditionalMode conditionalMode; 
    [SerializeField] List<Space> spaces = new List<Space>();

    public override bool Test(Game.Faction faction)
    {
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

        if (conditionalMode == ConditionalMode.More)
            return spaces.Where(space => space.flag == faction && space.conflictMarker == false).Count() >
                spaces.Where(space => space.flag == opposingFaction && space.conflictMarker == false).Count();
        else if (conditionalMode == ConditionalMode.All)
            return spaces.All(space => space.flag == faction && space.conflictMarker == false); 
        else
            return spaces.Any(space => space.flag == faction && space.conflictMarker == false);
    }
}
