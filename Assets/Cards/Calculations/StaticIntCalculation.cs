using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticIntCalculation : Calculation<int>
{
    public override int Calculate()
    {
        calculated = true;
        return value;
    }
}
