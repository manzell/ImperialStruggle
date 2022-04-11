using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitiativePhase : MonoBehaviour, IPhaseAction
{
    public static UnityEvent<InitiativePhase> InitiativePhaseStart = new UnityEvent<InitiativePhase>(), InitiativePhaseEnd = new UnityEvent<InitiativePhase>(); 
    public Game.Faction initiative; 
    public enum Response { Play, Pass }
    UnityAction callback; 

    public void Do(Phase phase, UnityAction callback)
    {
        if (VictoryPointTrack.VP > 15)
            Game.initiative = Game.Faction.France;
        if (VictoryPointTrack.VP < 15)
            Game.initiative = Game.Faction.Britain;

        initiative = Game.initiative; 
        InitiativePhaseStart.Invoke(this); 
        this.callback = callback;
    }

    [Sirenix.OdinInspector.Button(Name = "Play or Pass")]
    public void Select(Response response)
    {
        ActionRound[] actionRounds = GetComponentsInChildren<ActionRound>();
        Game.Faction opposingFaction = Game.initiative == Game.Faction.France ? Game.Faction.Britain : Game.Faction.France;

        for(int i = 0; i < actionRounds.Length; i++)
        {
            if (i % 2 == 0)
                actionRounds[i].actingFaction = response == Response.Play ? Game.initiative : opposingFaction;
            else
                actionRounds[i].actingFaction = response == Response.Play ? opposingFaction : Game.initiative;
        }

        Debug.Log($"{Game.initiative} elects to {(actionRounds[0].actingFaction == Game.initiative ? "Play" : "Pass")} the first Action Round");
        InitiativePhaseEnd.Invoke(this); 
        callback.Invoke(); 
    }
}
