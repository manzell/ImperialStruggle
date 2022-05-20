using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class WarTurn : SerializedMonoBehaviour
{
    public List<Theater> theaters;
    public Dictionary<Theater, Game.Faction> theaterWinners; 
}
