using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class ActionPoint : SerializedMonoBehaviour
{
    public Game.ActionType actionType;
    public Game.ActionTier actionTier;
    public Calculation<int> calculation;
    public List<IConditional> conditionals;

    public int actionPoints => calculation.value; 

    public int Value(List<ICriteria> criteria)
    {
        bool pass = true; 

        foreach(Object criterion in criteria)
        {
            foreach(IConditional condition in conditionals)
            {
                if (condition is Conditional<Space> spaceCondition)
                    pass &= spaceCondition.Test((Space)criterion);
                if (condition is Conditional<Market> marketCondition)
                    pass &= marketCondition.Test((Market)criterion);
                if (condition is Conditional<MilSpace> milCondition)
                    pass &= milCondition.Test((MilSpace)criterion);
                if (condition is Conditional<PoliticalSpace> politicalCondition)
                    pass &= politicalCondition.Test((PoliticalSpace)criterion);
                if (condition is Conditional<Map> mapCondition)
                    pass &= mapCondition.Test((Map)criterion);
            }
        }

        if (pass)
            return calculation.value;
        else
            return 0; 
    }
}
