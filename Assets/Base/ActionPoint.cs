using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

[System.Serializable]
public class ActionPoint
{
    public Game.ActionType actionType;
    public Game.ActionTier actionTier;
    public int actionPoints;
    public List<Conditional> conditionals;

    public int Value(PlayerAction context) => conditionals.All(condition => condition.Test(context)) ? actionPoints : 0;
}
