using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceConflictMarkerCommand : Command
{
    [SerializeField] Calculation<int> numSpaces;
    [SerializeField] Calculation<List<Space>> eligibleSpaces;

    public override void Do(Action action)
    {
        throw new System.NotImplementedException();
    }
}
