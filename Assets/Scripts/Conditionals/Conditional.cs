using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public interface Conditional
{
    public enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
    public ConditionType ConditionalType { get; }
    public bool Test(GameAction context);
    public string ConditionalText { get; }
}

public interface ICriteria { }