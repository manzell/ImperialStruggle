using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public abstract class Conditional : IConditional 
{
    protected enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
    public string conditionalText;
    public abstract bool Test(BaseAction context);
}

public interface IConditional { }
public interface ICriteria { }