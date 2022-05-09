using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

// Returns true if the Target Faction controls Any of the given Spaces, All of the given spaces, or More than the opposition
public class ControlCondition : Conditional
{
    public enum ConditionalMode { Any, All, More, Most }
    [SerializeField] ConditionalMode conditionalMode; 
    [SerializeField] List<Space> spaces = new List<Space>();
    [SerializeField] Game.Faction faction; 

    public override bool Test(BaseAction action)
    {
        Game.Faction opposingFaction = faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

        switch(conditionalMode)
        {
            case ConditionalMode.Any:
                return spaces.Any(space => space.control == faction);
            case ConditionalMode.All:
                return spaces.All(space => space.control == faction);
            case ConditionalMode.Most:
                return spaces.Count(space => space.control == faction) >= (spaces.Count / 2); 
            case ConditionalMode.More:
                return spaces.Count(space => space.control == faction) > spaces.Count(space => space.control == opposingFaction);
            default:
                return true; 
        }
    }
}
