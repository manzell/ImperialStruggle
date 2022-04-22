using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ControlCondition : Conditional
{
    public enum ConditionalMode { Any, All, More }
    [SerializeField] ConditionalMode conditionalMode; 
    [SerializeField] List<Space> spaces = new List<Space>();

    public override bool Test(Object _player)
    {
        if (_player is Player)
        {
            Game.Faction faction = (_player as Player).faction;
            Game.Faction opposingFaction = faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

            if (conditionalMode == ConditionalMode.More)
                return spaces.Where(space => space.flag == faction && space.conflictMarker == false).Count() >
                    spaces.Where(space => space.flag == opposingFaction && space.conflictMarker == false).Count();
            else if (conditionalMode == ConditionalMode.All)
                return spaces.All(space => space.flag == faction && space.conflictMarker == false);
            else
                return spaces.Any(space => space.flag == faction && space.conflictMarker == false);
        }
        return true; 
    }
}
