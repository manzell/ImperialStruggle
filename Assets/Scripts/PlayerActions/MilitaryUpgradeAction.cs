using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class MilitaryUpgradeAction : PlayerAction
{ 
    protected override void Do()
    {
        throw new System.NotImplementedException(); 
        //if(warTiles.Count > 0)
        //{
            //SelectionController.Selection selection = FindObjectOfType<SelectionController>().Select(warTiles, 1);
            //selection.SetTitle($"Select a {actingPlayer.faction} Basic War Tile to replace");
            //selection.callback = selectedTiles => Finish(selectedTiles);
        //}
    }

    void Finish(List<ISelectable> selectedTiles)
    {

    }
}
