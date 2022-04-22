using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CountSpacesCalculation : Calculation<int>
{
    [SerializeField] RangeInt range; 
    [SerializeField] List<Space> spaces;
    [SerializeField] Game.Faction targetFaction;

    public override int Calculate()
    {
        calculated = true;
        return Mathf.Clamp(spaces.Where(space => space.flag == targetFaction).Count(), range.start, range.end);
    }
}