using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Mathematics;

public class WarTurn : Phase
{
    public List<Theater> theaters;
    public Dictionary<Theater, Faction> theaterWinners;

    public override bool Completed => throw new System.NotImplementedException();
}
