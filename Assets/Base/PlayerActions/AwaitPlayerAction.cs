using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class AwaitPlayerAction : PlayerAction
{
    UnityAction callback; 
    public override void Do(UnityAction callback)
    {
        // Literally just do nothing. Look for the UI button and update it to point to our finish action round
        this.callback = callback;
    }

    [Button] void FinishActionRound() => callback.Invoke(); 
}
