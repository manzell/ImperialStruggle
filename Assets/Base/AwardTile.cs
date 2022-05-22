using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class AwardTile : SerializedMonoBehaviour
{
    public bool used;
    public int victoryPoints, treatyPoints, requiredMargin = 1;

    public List<Conditional> conditionals; 
    public List<Command> commands; 
}
