using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

// An ACTION sits on Advantage Tiles, Spaces, Ministers, and on the Player and allows them to Do A Thing
public class Action : MonoBehaviour, IExhaustable
{
    public Game.Faction actingFaction; 
    public Game.ActionType requiredActionType = Game.ActionType.None;
    public int actionCost = 0;
    public bool requireMajorAction; 
    public bool available;

    [HideInInspector]
    public UnityEvent<Action> Try = new UnityEvent<Action>(),
        CanEvent = new UnityEvent<Action>(), 
        DoEvent = new UnityEvent<Action>(); 

    bool _exhausted = false;
    bool IExhaustable.exhausted
    {
        get => _exhausted;
        set => _exhausted = value;
    }
}