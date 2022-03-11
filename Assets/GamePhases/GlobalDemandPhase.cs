using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System; 

public class GlobalDemandPhase : MonoBehaviour, IPhaseAction
{
    public static UnityEvent<PeaceTurn> setGlobalDemandEvent = new UnityEvent<PeaceTurn>();

    public void Do(Phase phase, UnityAction callback)
    {
        PeaceTurn peaceTurn = phase as PeaceTurn; 

        while(peaceTurn.globalDemandResources.Count < 3)
        {
            Game.Resource resource = (Game.Resource)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Game.Resource)).Length);

            if (!peaceTurn.globalDemandResources.Contains(resource))
                peaceTurn.globalDemandResources.Add(resource);
        }

        Debug.Log($"{peaceTurn.globalDemandResources[0]}, {peaceTurn.globalDemandResources[1]}, and {peaceTurn.globalDemandResources[2]} added to Global Demand");

        setGlobalDemandEvent.Invoke(peaceTurn);
        callback.Invoke(); 
    }
}
