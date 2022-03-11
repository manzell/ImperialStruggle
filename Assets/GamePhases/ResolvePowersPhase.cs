using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResolvePowersPhase : MonoBehaviour, IPhaseAction
{
    public static 
        UnityEvent<PeaceTurn> endOfPhaseEvent = new UnityEvent<PeaceTurn>();

    public void Do(Phase phase, UnityAction callback)
    {
        // TO DO - We actually need to call these events one-at-a-time based on Initiative, but we'll figure that out later
        endOfPhaseEvent.Invoke(Phase.currentPhase as PeaceTurn);
        callback.Invoke(); 
    }
}