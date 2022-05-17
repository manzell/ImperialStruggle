using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinistryCardCommand : Command
{
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<List<ISelectable>> ministryAction)
        {
            ministryAction.target.ForEach(card => { 
                (card as MinistryCard).ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
                Game.Log($"{(card as MinistryCard).faction} selects {card} Ministry Card");
            }); 
        }
    }
}
