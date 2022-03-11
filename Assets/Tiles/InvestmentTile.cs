using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class InvestmentTile : SerializedMonoBehaviour, IExhaustable
{
    public Dictionary<Game.ActionType, int> majorAction, minorAction; 
    public bool eventCardTrigger, milUpgradeTrigger;
    public bool available = true; 

    bool _exhausted = false; 
    public bool exhausted { 
        get => _exhausted; 
        set => _exhausted = value; 
    }
}
