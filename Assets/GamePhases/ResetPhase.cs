using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ResetPhase : MonoBehaviour, IPhaseAction
{
    Dictionary<IExhaustable, bool> prevState = new Dictionary<IExhaustable, bool>(), 
        nextState = new Dictionary<IExhaustable, bool>();

    public void Do(Phase phase, UnityAction callback)
    {
        prevState.Clear();
        foreach (IExhaustable tile in FindObjectsOfType<Object>().OfType<IExhaustable>())
        {
            prevState.Add(tile, tile.exhausted);

            if (tile.exhausted)
            {
                Debug.Log($"{tile} unexhausted");
                tile.exhausted = false;
            }
        }

        callback.Invoke(); 
    }

    public void Undo()
    {
        nextState.Clear(); 
        foreach(IExhaustable tile in prevState.Keys)
        {
            nextState.Add(tile, tile.exhausted); 
            tile.exhausted = prevState[tile];
        }
    }
}
