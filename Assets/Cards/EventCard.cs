using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class EventCard : SerializedMonoBehaviour, ICard
{
    [HideInInspector] public UnityAction callback;

    public Game.ActionType reqdActionType;
    public Game.Era era;
    public List<Command> commands = new List<Command>();

    public void Play(UnityAction callback)
    {
        //Game.Faction faction = (Phase.currentPhase as ActionRound).actingFaction;
        //this.callback = callback;

        //Debug.Log($"{this} played by {faction}");

        ////foreach(Command command in commands)
        ////    command.Do(); 

        //callback.Invoke();
    }
}
