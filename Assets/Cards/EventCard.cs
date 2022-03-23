using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public abstract class EventCard : SerializedMonoBehaviour, ICard
{
    [HideInInspector] public Game.Faction playFaction; 
    [HideInInspector] public UnityAction callback;

    public string bonusKeywords;
    public Game.ActionType reqdActionType;
    public List<Game.Era> eras;

    public Dictionary<Game.Faction, string> 
        commandText = new Dictionary<Game.Faction, string>(),
        bonusText = new Dictionary<Game.Faction, string>(); 

    public void Play(UnityAction callback)
    {
        this.callback = callback;
        Debug.Log($"{this} played by {(Phase.currentPhase as ActionRound).actingFaction}");
        PlayCard(Phase.currentPhase as ActionRound);
    }

    public abstract void PlayCard(ActionRound actionRound);
}
