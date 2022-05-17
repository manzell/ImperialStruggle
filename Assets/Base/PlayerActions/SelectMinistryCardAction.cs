using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 
using System.Linq; 

public class SelectMinistryCardAction : PlayerAction, ITargetType<List<ISelectable>>
{
    UnityAction callback;
    SelectionController.Selection selection;

    public List<ISelectable> target => selection.selectableItems
        .Where(kvp => kvp.Value == SelectionController.Selection.ItemSelectStatus.Selected)
        .Select(kvp => kvp.Key).ToList(); 

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;
        List<MinistryCard> ministers = player.ministers.Where(minister => minister.eras.Contains(Phase.currentPhase.era)).OrderBy(minister => minister.name).ToList();
        // Think on how to make this work for future turns. 

        selection = FindObjectOfType<SelectionController>().Select(ministers.ToList<ISelectable>(), 2 - player.ministers.Where(card => card.ministryCardStatus == MinistryCard.MinistryCardStatus.Selected).Count());
        selection.SetTitle($"Select {player.faction} Ministry Cards"); 
        selection.callback = Finish; 
    }

    [Button]
    void Finish(List<ISelectable> cards)
    {
        base.Do(() => { });
        callback.Invoke();
    }
}
