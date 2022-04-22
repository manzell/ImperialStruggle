using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

// An ACTION sits on Advantage Tiles, Spaces, Ministers, and on the Player and allows them to Do A Thing
public class ActionOld : SerializedMonoBehaviour, IExhaustable
{
    public string actionName; 
    public Game.Faction actingFaction; 
    public Game.ActionType requiredActionType = Game.ActionType.None;
    public Game.ActionTier requiredActionTier = Game.ActionTier.Minor; 
    public int actionCost = 0;

    public Dictionary<(Game.ActionType, Game.ActionTier), int> _actionCost = new Dictionary<(Game.ActionType, Game.ActionTier), int>(),
        finalActionCost = new Dictionary<(Game.ActionType, Game.ActionTier), int>();

    public bool requireMajorAction; 
    public bool available;

    [HideInInspector]
    public UnityEvent<ActionOld> Try = new UnityEvent<ActionOld>(),
        CanEvent = new UnityEvent<ActionOld>(), 
        DoEvent = new UnityEvent<ActionOld>(); 

    bool IExhaustable.exhausted { get; set; }

    public virtual bool Can(Game.Faction faction) => true;
    public virtual void Do(Game.Faction faction) { }
}