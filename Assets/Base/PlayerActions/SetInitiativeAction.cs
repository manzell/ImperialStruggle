using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class SetInitiativeAction : PlayerAction
{
    UnityAction callback;
    SelectionController selectionController;
    public Game.Faction selectedFaction; 

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;
        selectionController = FindObjectOfType<SelectionController>();

        selectionController.Summon(this, Player.players.Keys.ToList(), 2);
        selectionController.SetTitle(actionText);
    }

    // How does the Selection Controller work? 
    // We summon it with the list of things (we need to implement a better Visitor Pattern for this)
    // We also pass in a Callback for OK, as well as Cancel. 

    // We pass the callback in and it calls 


    [Button]
    void Finish()
    {
        // Let's assume for the time being that this only occurs within a Peace Turn phase. 
        Game.Faction actingFaction = VictoryPointTrack.VP >= 15 ? Game.Faction.France : Game.Faction.Britain;
        ActionRound[] actionRounds = Phase.currentPhase.GetComponentsInChildren<ActionRound>();
        Game.Faction opposingFaction = selectedFaction == Game.Faction.France ? Game.Faction.Britain : Game.Faction.France;

        Debug.Log($"{actingFaction} elects to {(actingFaction == selectedFaction ? "Play" : "Pass")} the first Action Round");

        for (int i = 0; i < actionRounds.Length; i++) 
        { 
            actionRounds[i].actingFaction = i % 2 == 0 ? selectedFaction : opposingFaction;
        }

        base.Do(callback);
    }
}