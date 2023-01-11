using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Calculation<T>
    {
        protected bool calculated = false;

        protected abstract T Calc(Player player);
        public T Calculate(Player player)
        {
            calculated = true;
            return Calc(player); 
        }
    }
}