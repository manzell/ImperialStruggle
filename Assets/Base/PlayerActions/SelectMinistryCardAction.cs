using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 
using System.Linq; 

public class SelectMinistryCardAction : PlayerAction
{
    public List<MinistryCard> selectedCards;    
    UnityAction callback;
    SelectionController.Selection<MinistryCard> selection;

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;
        selection = FindObjectOfType<SelectionController>().Select(player.ministers, 2);
        selection.callback = Finish; 
    }

    [Button]
    void Finish(List<MinistryCard> cards)
    {
        selectedCards = cards; 
 
        base.Do(() => { });
        callback.Invoke();
    }
}
