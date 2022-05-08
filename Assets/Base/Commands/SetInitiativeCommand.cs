using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitiativeCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is SetInitiativeAction initiativeAction)
        {
            Phase.currentPhase.GetComponent<PeaceTurn>().initiative = initiativeAction.selectedFaction; 
        }        
    }
}