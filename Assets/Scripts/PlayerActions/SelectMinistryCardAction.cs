using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 
using System.Linq; 

public class SelectMinistryCardAction : PlayerAction
{
    SelectionController<MinistryCard>.Selection selection;

    protected override void Do()
    {
        /*
        List<MinistryCard> ministers = player.ministers.Where(minister => minister.eras.Contains(Phase.CurrentPhase.era)).OrderBy(minister => minister.name).ToList();
        // Think on how to make this work for future turns. 

        selection = FindObjectOfType<SelectionController>().Select(ministers.ToList<ISelectable>(), 2 - player.ministers.Where(card => card.ministryCardStatus == MinistryCard.MinistryCardStatus.Selected).Count());
        selection.SetTitle($"Select {player.faction} Ministry Cards"); 
    */
    }
}