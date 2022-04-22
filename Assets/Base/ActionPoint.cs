using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

[System.Serializable]
public class ActionPoint
{
    public Game.ActionType actionType;
    public Game.ActionTier actionTier;
    public Calculation<int> calculation;

    [SerializeField] public List<Conditional> conditionals;

    public int actionPoints => calculation.value; 

    public int Value(List<ICriteria> criteria)
    {
        bool pass = true; 

        foreach(Object criterion in criteria)
            foreach(Conditional condition in conditionals)
                    pass &= condition.Test(criterion);

        if (pass)
            return calculation.value;
        else
            return 0; 
    }
}
