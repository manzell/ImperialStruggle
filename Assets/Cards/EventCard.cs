using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class EventCard : SerializedMonoBehaviour, ICard
{
    [HideInInspector] public Game.Faction playFaction; 
    [HideInInspector] public UnityAction callback;

    public Game.ActionType reqdActionType;
    public List<Game.Era> eras;

    public void Play(UnityAction callback)
    {
        this.callback = callback;
        Debug.Log($"{this} played by {(Phase.currentPhase as ActionRound).actingFaction}");
        PlayCard(Phase.currentPhase as ActionRound);
    }

    public virtual void PlayCard(ActionRound actionRound)
    {
        foreach(CardEvent cardEvent in GetComponents<CardEvent>())
        {
            if (cardEvent.eventable && cardEvent.faction == Game.Faction.Neutral || cardEvent.faction == actionRound.actingFaction)
                cardEvent.Event();
        }

        callback.Invoke();
    }
}
