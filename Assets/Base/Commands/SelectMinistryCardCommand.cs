using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SelectMinistryCardCommand : Command
{
    public static UnityEvent<MinistryCard> selectMinistryCardEvent = new UnityEvent<MinistryCard>(); 
    public override void Do(BaseAction action)
    {
        if(action is ITargetType<List<ISelectable>> ministryAction)
        {
            foreach(MinistryCard card in ministryAction.target)
            {
                card.ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
                selectMinistryCardEvent.Invoke(card);

                Game.Log($"{card.faction} selects {card.name} Ministry Card");
            }
        }
    }
}