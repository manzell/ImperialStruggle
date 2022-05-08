using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinistryCardCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is SelectMinistryCardAction ministryAction)
        {
            ministryAction.selectedCards.ForEach(card => { 
                card.ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
                Game.Log($"{ministryAction.player.faction} selects {card} Ministry Card");
            }); 
        }
    }
}
