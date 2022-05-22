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

    public Map map; 
    public Game.Faction flag;
    public Game.Faction control => conflictMarker ? Game.Faction.Neutral : flag; 
    public Game.Era availableEra;
    public List<Space> adjacentSpaces;
    public int flagCost;
    public bool prestige, alliance, conflictMarker;
    public List<PlayerAction> standardActions = new List<PlayerAction>(); 

    [Button] void Flag(Game.Faction faction)
    {
        flag = faction;
        updateSpaceEvent.Invoke(); 
    }
}