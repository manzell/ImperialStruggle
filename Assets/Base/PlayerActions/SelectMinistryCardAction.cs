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
    SelectionController selectionController;

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;
        selectionController = FindObjectOfType<SelectionController>();
        // We need to open up a choice dialog
        // Provide it all eligible Ministry Cards for the current end 
        // Give it a number to select
        //selectionWindowController.Summon(availableMinistryCards, 2, FindObjectOfType<UI_SelectionWindow>().transform);

        selectionController.Summon(player.ministers, 2);
        selectionController.SetTitle(actionText);

        // Send our callback to the OK Method of the selectionController
        // Make sure that the selection Controller knows where to get it's data. 


        //Finish(callback); 
    }

    [Button]
    void Finish()
    {
        selectionController.Dismiss(); 
        base.Do(() => { });
        callback.Invoke();
    }
}
