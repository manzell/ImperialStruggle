using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class SetInitiativeAction : PlayerAction, ITargetType<Game.Faction>, ITargetType<ActionRound>
{
    UnityAction callback;
    SelectionController.Selection<Player> selection;
    Game.Faction selectedFaction;
    ActionRound selectedActionRound; 

    public Game.Faction target => selectedFaction;
    ActionRound ITargetType<ActionRound>.target => selectedActionRound;

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;

        player = Player.players[FindObjectOfType<RecordsTrack>().VictoryPoints >= 15 ? Game.Faction.France : Game.Faction.Britain]; 
        selection = FindObjectOfType<SelectionController>().Select(Player.players.Values.ToList(), 1);
        selection.SetTitle($"{player.faction} selects Initiative"); 
        selection.callback = Finish;
    }

    void Finish(List<Player> players)
    {
        // Let's assume for the time being that this only occurs within a Peace Turn phase. 
        ActionRound[] actionRounds = Phase.currentPhase.GetComponentsInChildren<ActionRound>();
        selectedFaction = players.First().faction;

        Debug.Log($"{player.faction} elects to {(player.faction == selectedFaction ? "Play" : "Pass")} the first Action Round; " +
            $"{player.faction.Opposition()} will go {(player.faction.Opposition() == selectedFaction ? "First" : "Second")}");

        Phase.currentPhase.GetComponent<PeaceTurn>().initiative = selectedFaction;

        for (int i = 0; i < actionRounds.Length; i++) 
        {
            selectedFaction = i % 2 == 0 ? selectedFaction : selectedFaction.Opposition();
            selectedActionRound = actionRounds[i]; 
            base.Do(() => { });             
        }

        callback.Invoke(); 
    }
}