using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class SetInitiativeAction : PlayerAction
{
    UnityAction callback;
    SelectionController selectionController;
    [SerializeField] Game.Faction selectedFaction; 

    protected override void Do(UnityAction callback)
    {
        selectionController = FindObjectOfType<SelectionController>();
        this.callback = callback;

        selectionController.Summon(new List<Game.Faction>() { Game.Faction.Britain, Game.Faction.France }, 2);
        selectionController.SetTitle(actionText);
    }

    [Button]
    void Finish()
    {
        // Let's assume for the time being that this only occurs within a Peace Turn phase. 
        Game.Faction actingFaction = VictoryPointTrack.VP >= 15 ? Game.Faction.France : Game.Faction.Britain;
        ActionRound[] actionRounds = Phase.currentPhase.GetComponentsInChildren<ActionRound>();
        Game.Faction opposingFaction = selectedFaction == Game.Faction.France ? Game.Faction.Britain : Game.Faction.France;

        Debug.Log($"{actingFaction} elects to {(actingFaction == selectedFaction ? "Play" : "Pass")} the first Action Round");

        for (int i = 0; i < actionRounds.Length; i++)
            actionRounds[i].actingFaction = i % 2 == 0 ? selectedFaction : opposingFaction;

        callback.Invoke();
    }
}