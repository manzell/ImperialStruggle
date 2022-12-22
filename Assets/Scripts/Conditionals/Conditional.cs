using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using JetBrains.Annotations;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Conditional
    {
        public enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
        [field: SerializeField] public ConditionType ConditionalType { get; }
        [field: SerializeField] public string ConditionalText { get; }

        public abstract bool Test(GameAction context);
    }
    /*
    public interface IConditional
    {
        public bool Test(GameAction context);
        public Conditional.ConditionType ConditionalType { get; }
        public string ConditionalText { get; }
    }
    */
}