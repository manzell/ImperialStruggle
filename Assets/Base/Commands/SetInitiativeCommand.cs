using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitiativeCommand : Command
{
    public override void Do(Action action)
    {
        if(action is SetInitiativeAction initiativeAction)
        {
            PeaceTurn peaceTurn = initiativeAction.GetComponent<PeaceTurn>();
            peaceTurn.initiative = initiativeAction.player.faction; 
        }        
    }
}