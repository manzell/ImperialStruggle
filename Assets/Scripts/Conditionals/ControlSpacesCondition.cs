using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore;

namespace ImperialStruggle
{
    // Returns true if the Target Faction controls Any of the given Spaces, All of the given spaces, or More than the opposition
    public class ControlSpacesCondition : Conditional<IAction>
    {
        public enum ComparisonType { Any, All, More, Most, None }
        [SerializeField] ComparisonType conditionalMode;
        [SerializeField] SpacesCalc spacesCalc; 

        protected override bool Test(IAction action)
        {
            Faction faction = (action as PlayerAction).Player.Faction;
            IEnumerable<Space> spaces = spacesCalc.Calculate(action).Select(data => Game.SpaceLookup[data as SpaceData]);  

            switch (conditionalMode)
            {
                case ComparisonType.Any:
                    return spaces.Any(space => space.Control == faction);
                case ComparisonType.All:
                    return spaces.All(space => space.Control == faction);
                case ComparisonType.Most:
                    return spaces.Count(space => space.Control == faction) >= (spaces.Count() / 2);
                case ComparisonType.More:
                    return spaces.Count(space => space.Control == faction) > 
                        spaces.Count(space => space.Control == faction.Opposition());
                case ComparisonType.None:
                    return !spaces.Any(space => space.Control == faction); 
                default:
                    return true;
            }
        }
    }
}