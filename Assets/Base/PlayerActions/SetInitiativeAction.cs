using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class SetInitiativeAction : PlayerAction, ITargetType<Player>, ITargetType<ActionRound>
{
    UnityAction callback;
    SelectionController.Selection selection;
    Player selectedPlayer;
    ActionRound selectedActionRound; 

    ActionRound ITargetType<ActionRound>.target => selectedActionRound;
    Player ITargetType<Player>.target => selectedPlayer;

    public override bool Can()
    {
        if(player == null) 
            player = Player.players[RecordsTrack.VictoryPoints >= 15 ? Game.Faction.Britain : Game.Faction.France];
        return base.Can();
    }

    public override void Do(UnityAction callback)
    {
        this.callback = callback;

        selection = FindObjectOfType<SelectionController>().Select(Player.players.Values.ToList<ISelectable>(), 1);
        selection.SetTitle($"{player.faction} selects Initiative"); 
        selection.callback = Finish;
    }

    void Finish(List<ISelectable> players)
    {
        // Let's assume for the time being that this only occurs within a Peace Turn phase. 
        ActionRound[] actionRounds = Phase.currentPhase.GetComponentsInChildren<ActionRound>();
        selectedPlayer = (Player)players.First();

        Debug.Log($"{player.faction} elects to {(player == selectedPlayer ? "Play" : "Pass")} the first Action Round; " +
            $"{player.faction.Opposition()} will go {(player.faction.Opposition() == selectedPlayer.faction ? "First" : "Second")}");

        Phase.currentPhase.GetComponent<PeaceTurn>().initiative = selectedPlayer;

        for (int i = 0; i < actionRounds.Length; i++) 
        {
            selectedPlayer = i % 2 == 0 ? selectedPlayer : Player.players[selectedPlayer.faction.Opposition()];
            selectedActionRound = actionRounds[i]; 
            base.Do(() => { });             
        }

        callback.Invoke(); 
    }
}