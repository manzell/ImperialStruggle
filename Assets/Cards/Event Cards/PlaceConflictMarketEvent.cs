using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceConflictMarketEvent : CardEvent
{
    [SerializeField] List<Space> eligibleSpaces;
    [SerializeField] int count = 1; 

    public override void Event()
    {
        // Todo: Select Window for placing Conflict Markers
    }
}
