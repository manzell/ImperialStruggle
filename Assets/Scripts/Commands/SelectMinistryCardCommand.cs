using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine; 

public class SelectMinistryCardCommand : Command
{
    public static UnityEvent<MinistryCard> selectMinistryCardEvent = new UnityEvent<MinistryCard>(); 
    public override void Do(GameAction action)
    {
        if(action is ActionTarget<List<ISelectable>> ministryAction)
        {
            foreach(MinistryCard card in ministryAction.target)
            {
                card.ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
                selectMinistryCardEvent.Invoke(card);

                Debug.Log($"{card.faction} selects {card.name} Ministry Card");
            }
        }
    }
}