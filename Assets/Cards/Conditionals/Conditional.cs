using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public abstract class Conditional : SerializedMonoBehaviour
{
    public string conditionalText; 
    public abstract bool Test(Game.Faction faction); 
}
