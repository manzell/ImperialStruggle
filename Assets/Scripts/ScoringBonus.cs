using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScoringBonus : MonoBehaviour
{
    public string bonusName; 
    public abstract Faction scoringFaction { get; }
    public abstract int bonusValue { get; }
}
