using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
/*
public class SpaceCalculation : Calculation<List<Space>>
{
    enum SpaceType { Any, Market, Political, Military }

    [SerializeField] SpaceType spaceType = SpaceType.Any;

    public override List<Space> Calculate()
    {
        IEnumerable<Space> spaces = FindObjectsOfType<Space>(); 

        switch (spaceType)
        {
            case SpaceType.Market:
                return spaces.Where(space => space is Market).ToList();
            case SpaceType.Political:
                return spaces.Where(space => space is PoliticalSpace).ToList();
            case SpaceType.Military:
                return spaces.Where(space => space is Fort).ToList();
            default:
                return spaces.ToList(); 
        }
    }
}
*/