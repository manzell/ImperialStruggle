using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[System.Serializable]
public class Space
{
    [HideInInspector]
    public UnityEvent
        updateSpaceEvent = new UnityEvent();

    public SpaceData data; 

    public Faction flag { get; private set; }
    public bool conflictMarker { get; private set; }
    public Faction control => conflictMarker ? null : flag;

    public string name => data.spaceName;
    public Map map => data.map;
    public Game.Era availableEra => data.availableEra;
    public List<Space> adjacentSpaces => data.adjacentSpaces;
    public int flagCost => data.flagCost;
    public bool prestige => data.prestige; 
    public bool alliance => alliance;

    public List<PlayerAction> standardActions = new List<PlayerAction>(); // Rethink how this system works? 
        // Maybe the Standard Actions exist on the player, and they're passed in the Space as a context? 

    [Button] void Flag(Faction faction)
    {
        flag = faction;
        updateSpaceEvent.Invoke(); 
    }

    public void SetConflictMarker(bool status) => conflictMarker = status;
    public void SetFlag(Faction faction) => flag = faction; 
}