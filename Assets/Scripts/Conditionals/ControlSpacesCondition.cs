using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore;

namespace ImperialStruggle
{
    // Returns true if the Target Faction controls Any of the given Spaces, All of the given spaces, or More than the opposition
    public class ControlSpacesCondition : Conditional<Space>
    {
        public enum ComparisonType { Any, All, More, Most, None }
        [SerializeField] ComparisonType conditionalMode;
        [SerializeField] List<SpaceData> spaces = new();
        [SerializeField] Faction faction; 

        protected override bool Test(Space space)
        {
            switch (conditionalMode)
            {
                case ComparisonType.Any:
                    return spaces.Any(space => Game.SpaceLookup[space].Control == faction);
                case ComparisonType.All:
                    return spaces.All(space => Game.SpaceLookup[space].Control == faction);
                case ComparisonType.Most:
                    return spaces.Count(space => Game.SpaceLookup[space].Control == faction) >= (spaces.Count / 2);
                case ComparisonType.More:
                    return spaces.Count(space => Game.SpaceLookup[space].Control == faction) > 
                        spaces.Count(space => Game.SpaceLookup[space].Control == faction.Opposition());
                case ComparisonType.None:
                    return !spaces.Any(space => Game.SpaceLookup[space].Control == faction); 
                default:
                    return true;
            }
        }
    }
}