using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class StaticIntCalculation : Calculation<int>
    {
        [SerializeField] int value;

        protected override int Calc(Player player) => value; 
    }
}