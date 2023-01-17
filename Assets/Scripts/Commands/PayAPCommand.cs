using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class PayAPCommand : Command
    {
        public override void Do(IAction context)
        {
            /*
            ActionPoints playerAP = (action as PlayerAction).player.actionPoints;  
            ActionPoints apCosts = new ActionPoints((action as PlayerAction).actionPointCost);

            foreach (ActionPoint apCost in apCosts.Where(ap => ap.baseValue > 0))
            {
                foreach (ActionPoint ap in playerAP.Where(playerAP => playerAP >= apCost && playerAP.Value((action as PlayerAction)) > 0)
                    .OrderBy(ap => ap.conditionText)
                    .ThenByDescending(key => key.actionTier)
                    .ThenByDescending(key => key.actionType <= ActionPoint.ActionType.Military))
                {
                    if (apCost.baseValue > 0)
                    {
                        int amtToCharge = Mathf.Min(apCost.baseValue, ap.Value((action as PlayerAction)));
                        Debug.Log($"{(action as PlayerAction).player.name} paying {amtToCharge} of {ap.name} for {apCost.name} [{action.actionName}]");

                        apCost.baseValue -= amtToCharge;
                        ap.baseValue -= amtToCharge;

                        if (ap.baseValue <= 0)
                            playerAP.Remove(ap);

                        AdjustAPCommand.adjustAPEvent.Invoke(); 
                    }
                }
            }
            */

            throw new System.NotImplementedException();
        }
    }
}