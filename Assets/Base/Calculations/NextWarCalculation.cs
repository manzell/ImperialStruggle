using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class NextWarCalculation : Calculation<Phase>
{
    public override Phase Calculate()
    {
        Phase phase = Phase.currentPhase;

        if (phase == null)
            return null; 

        while(!(phase is Theater))
        {
            phase = phase.nextPhase; 
        }

        return phase is Theater ? phase.parentPhase : null; 
    }
}
