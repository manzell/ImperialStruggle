using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public abstract class EventCard : SerializedMonoBehaviour, ICard
{
    [HideInInspector] public UnityAction callback;

    public string bonusKeywords;
    public Game.ActionType reqdActionType;
    public List<Game.Era> eras;

    public Dictionary<Game.Faction, string> 
        commandText = new Dictionary<Game.Faction, string>(),
        bonusText = new Dictionary<Game.Faction, string>(); 

    public void PlayCard(UnityAction callback)
    {
        this.callback = callback;
        Debug.Log($"{this} played by {(Phase.currentPhase as ActionRound).actingFaction}");
        Play(Phase.currentPhase as ActionRound); 
    }

    public abstract void Play(ActionRound actionRound);
}
