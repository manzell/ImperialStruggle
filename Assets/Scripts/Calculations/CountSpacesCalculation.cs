using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CountSpacesCalculation : Calculation<int>
{
    [SerializeField] List<Space> spaces;
    [SerializeField] Faction targetFaction;

    public override int Calculate()
    {
        calculated = true;
        return Mathf.Max(spaces.Where(space => space.flag == targetFaction).Count(), 0); 
    }
}