using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    // Returns true if the Target Faction controls Any of the given Spaces, All of the given spaces, or More than the opposition
    public class ControlSpacesCondition : Conditional
    {
        public enum ConditionalMode { Any, All, More, Most }
        [SerializeField] ConditionalMode conditionalMode;
        [SerializeField] List<Space> spaces = new List<Space>();
        [SerializeField] Faction faction;

        public override bool Test(GameAction action)
        {
            Faction opposingFaction = faction == Game.Britain ? Game.France : Game.Britain;

            switch (conditionalMode)
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
}