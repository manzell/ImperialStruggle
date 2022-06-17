using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndARAction : PlayerAction
{
    public override void Do(UnityAction callback)
    {
        ActionRound ar = Phase.currentPhase.GetComponent<ActionRound>();
        Debug.Log($"{player} ended their Action Round"); 
    }

}
