using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Conditional
    {
        public enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
        public ConditionType ConditionalType { get; }
        public abstract bool Test(GameAction context);
        public string ConditionalText { get; }
    }
}