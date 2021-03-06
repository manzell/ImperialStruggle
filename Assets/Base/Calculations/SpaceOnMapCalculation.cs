using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceOnMapCalculation : Calculation<List<Space>>
{
    [SerializeField] List<Map> eligibleMaps = new List<Map>();

    public override List<Space> Calculate()
    {
        List<Space> spaces = new List<Space>();

        foreach (Map map in eligibleMaps)
            spaces.AddRange(map.spaces);

        return spaces; 
    }
}
