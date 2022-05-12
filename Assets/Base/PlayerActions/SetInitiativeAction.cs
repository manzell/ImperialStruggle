using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class SetInitiativeAction : PlayerAction
{
    UnityAction callback;
    SelectionController.Selection<Player> selection;
    public Game.Faction selectedFaction; 

    protected override void Do(UnityAction callback)
    {
        this.callback = callback;

        player = Player.players[FindObjectOfType<RecordsTrack>().VictoryPoints >= 15 ? Game.Faction.France : Game.Faction.Britain]; 
        selection = FindObjectOfType<SelectionController>().Select(Player.players.Values.ToList(), 1);
        selection.SetTitle($"{player.faction} selects Initiative"); 
        selection.callback = Finish;
    }

    [Button]
    void Finish(List<Player> players)
    {
        // Let's assume for the time being that this only occurs within a Peace Turn phase. 
        ActionRound[] actionRounds = Phase.currentPhase.GetComponentsInChildren<ActionRound>();
        Game.Faction opposingFaction = player.faction == Game.Faction.France ? Game.Faction.Britain : Game.Faction.France;
        selectedFaction = players.First().faction;

        Debug.Log($"{player.faction} elects to {(player.faction == selectedFaction ? "Play" : "Pass")} the first Action Round; " +
            $"{opposingFaction} will go {(opposingFaction == selectedFaction ? "First" : "Second")}");        

        for (int i = 0; i < actionRounds.Length; i++) 
        {
            base.Do(() => { }); 
            actionRounds[i].actingFaction = i % 2 == 0 ? selectedFaction : opposingFaction; // TODO - make this a gamestate altering command and bring an empty base.do
            // Optional: Change the acting player on all Actions in the phase? 
        }

        callback.Invoke(); 
    }
}