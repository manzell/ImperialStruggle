using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector; 

public class Space : MonoBehaviour, ICriteria, ISelectable
{
    [HideInInspector]
    public UnityEvent
        updateSpaceEvent = new UnityEvent();

    public SpaceData data; 
    public Game.Faction flag;
    public bool conflictMarker;
    public Game.Faction control => conflictMarker ? Game.Faction.Neutral : flag;

    public new string name => data.spaceName;
    public Map map => data.map;
    public Game.Era availableEra => data.availableEra;
    public List<Space> adjacentSpaces => data.adjacentSpaces;
    public int flagCost => data.flagCost;
    public bool prestige => data.prestige; 
    public bool alliance => alliance;

    public List<PlayerAction> standardActions = new List<PlayerAction>(); // Rethink how this system works? 
        // Maybe the Standard Actions exist on the player, and they're passed in the Space as a context? 

    [Button] void Flag(Game.Faction faction)
    {
        flag = faction;
        updateSpaceEvent.Invoke(); 
    }
}