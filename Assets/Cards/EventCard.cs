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

    public void Play(UnityAction callback)
    {
        ActionRound actionRound = Phase.currentPhase as ActionRound;
        HashSet<GameObject> gameObjects = new HashSet<GameObject>();
        this.callback = callback;

        Debug.Log($"{this} played by {actionRound.actingFaction}");

        foreach(Command command in GetComponentsInChildren<Command>().Where(command => command.transform.parent == transform))
            gameObjects.Add(command.gameObject);

        foreach(GameObject commandSet in gameObjects)
        {
            bool doExecuteCommand = true;

            foreach (Conditional<Game.Faction> conditional in commandSet.GetComponents<Conditional<Game.Faction>>())
                doExecuteCommand &= conditional.Test(actionRound.actingFaction);

            if (doExecuteCommand)
            {
                foreach(Command command in commandSet.GetComponents<Command>())
                {
                    command.Do(actionRound.actingFaction); 
                }
            }
        }

        callback.Invoke();
    }
}
