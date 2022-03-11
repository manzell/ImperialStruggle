using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class SpendActionPointsPhase : MonoBehaviour, IPhaseAction
{
    UnityAction callback; 
    public void Do(Phase phase, UnityAction callback)
    {
        Debug.Log("Spend your Action Points"); 
        this.callback = callback;
    }

    [Button(Name ="Finish Action Round")] public void EndActionRound()
    {
        callback.Invoke(); 
    }
}
