using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class AdjustAPCommand : Command
{
    public static UnityEvent adjustAPEvent = new UnityEvent();

    public override void Do(BaseAction action)
    {
        // How do we determine if this is a 
        if(action is ITargetType<ActionPoints> APAction && action is ITargetType<Player> playerAction)
        {
            foreach (ActionPoint ap in APAction.target)
            {
                ActionPoint matchingAP = playerAction.target.actionPoints.First(_ap => ap.Equals(_ap));

                if (matchingAP != null)
                    matchingAP.baseValue += ap.baseValue; 
                else
                    playerAction.target.actionPoints.Add(ap);
                Debug.Log($"Adding {ap.name} to {playerAction.target} Action Point Pool");
            }
            adjustAPEvent.Invoke();
        }
        else if(action is IAdjustAP apAction)
        {
            foreach (ActionPoint ap in apAction.actionPoints)
            {
                ActionPoint matchingAP = apAction.actionPoints.First(_ap => ap.Equals(_ap));

                if (matchingAP != null)
                    matchingAP.baseValue += ap.baseValue;
                else
                    apAction.actionPoints.Add(ap);
                Debug.Log($"Adding {ap.name} to {apAction.player} Action Point Pool");
            }
            adjustAPEvent.Invoke(); // the actual AP is passed in - and can be modified
        }
    }
}