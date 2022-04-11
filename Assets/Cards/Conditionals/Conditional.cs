using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Conditional<T> : SerializedMonoBehaviour, IConditional 
{
    protected enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
    public string conditionalText;
    public abstract bool Test(T t);

    public System.Type Type() => typeof(T); 
}

public interface IConditional { }
public interface ICriteria { }