using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine; 

public class SelectMinistryCardCommand : Command
{
    MinistryCard card; 
    public static System.Action<MinistryCard> SelectEvent;

    public SelectMinistryCardCommand(MinistryCard card) => this.card = card; 

    public override void Do(GameAction action)
    {
        card.ministryCardStatus = MinistryCard.MinistryCardStatus.Selected;
        SelectEvent?.Invoke(card);
    }

    public override void Undo()
    {
        card.ministryCardStatus = MinistryCard.MinistryCardStatus.Reserved;
    }
}