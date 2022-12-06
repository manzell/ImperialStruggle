using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class SetInitiativeAction : PlayerAction
{
    SelectionController.Selection selection;

    protected override void Do()
    {
        selection = FindObjectOfType<SelectionController>().Select(Player.players.ToList<ISelectable>(), 1);

        selection.SetTitle($"{actingPlayer.faction} selects Initiative"); 
        selection.callback = Finish;
    }

    void Finish(List<ISelectable> players)
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            ActionRound[] actionRounds = Phase.CurrentPhase.GetComponentsInChildren<ActionRound>();
            peaceTurn.initiative = ((Player)players.First()).faction; 

            Debug.Log($"{actingPlayer.faction} elects to {(actingPlayer == peaceTurn.initiative ? "Play" : "Pass")} the first Action Round; " +
                $"{actingPlayer.faction.Opposition()} will go {(actingPlayer.faction.Opposition() == peaceTurn.initiative ? "First" : "Second")}");

            Debug.Log("Use a command to change the initiative!");

            for (int i = 0; i < actionRounds.Length; i++)
                actionRounds[i].actingFaction = i % 2 == 0 ? peaceTurn.initiative : peaceTurn.initiative.Opposition(); 
        }
    }
}