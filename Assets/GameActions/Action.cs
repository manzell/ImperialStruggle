using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

// An ACTION sits on Advantage Tiles, Spaces, Ministers, and on the Player and allows them to Do A Thing
public class Action : SerializedMonoBehaviour, IExhaustable
{
    public string actionName; 
    public Game.Faction actingFaction; 
    public Game.ActionType requiredActionType = Game.ActionType.None;
    public int actionCost = 0;

    public Dictionary<(Game.ActionType, Game.ActionTier), int> _actionCost = new Dictionary<(Game.ActionType, Game.ActionTier), int>(),
        finalActionCost = new Dictionary<(Game.ActionType, Game.ActionTier), int>();

    public bool requireMajorAction; 
    public bool available;

    [HideInInspector]
    public UnityEvent<Action> Try = new UnityEvent<Action>(),
        CanEvent = new UnityEvent<Action>(), 
        DoEvent = new UnityEvent<Action>(); 

    bool _exhausted = false;
    bool IExhaustable.exhausted { get; set; }

    public virtual bool Can(Game.Faction faction) => true;
    public virtual void Do(Game.Faction faction) { }
}