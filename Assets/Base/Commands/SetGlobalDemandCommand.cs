using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System; 

public class SetGlobalDemandCommand : Command
{
    public static UnityEvent<PeaceTurn> setGlobalDemandEvent = new UnityEvent<PeaceTurn>();

    public override void Do(BaseAction action)
    {

        if(action.TryGetComponent(out PeaceTurn peaceTurn))
        {
            while (peaceTurn.globalDemandResources.Count < 3)
            {
                Game.Resource resource = (Game.Resource)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Game.Resource)).Length);

                if (!peaceTurn.globalDemandResources.Contains(resource))
                    peaceTurn.globalDemandResources.Add(resource);
            }

            Game.Log($"{peaceTurn.globalDemandResources[0]}, {peaceTurn.globalDemandResources[1]}, and {peaceTurn.globalDemandResources[2]} added to Global Demand");

            setGlobalDemandEvent.Invoke(peaceTurn);
        }
    }
}
