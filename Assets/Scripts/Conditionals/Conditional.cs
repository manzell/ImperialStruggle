using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{

    // A Condition is a run-time calculation before which a PLAYER ACTION may be executed
    // Is a Conditional anything different than a Calculation<Bool>?

    [System.Serializable]
    public abstract class Conditional<T>
    {
        [field: SerializeField] public string Name { get; }

        public bool Check(T test)
        {
            bool retVal = Test(test);
            Debug.Log($"Checking {Name}: {retVal}");

            return retVal;
        }

        protected abstract bool Test(T context);
    }
}