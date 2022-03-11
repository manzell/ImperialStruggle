using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameEvents : SerializedMonoBehaviour
{
    public static UnityEvent gameStart = new UnityEvent(),
        turnStartEvent = new UnityEvent(),
        TurnEndEvent = new UnityEvent();

    public static UnityEvent<Game.Faction, InvestmentTile> 
        actionRoundStartEvent = new UnityEvent<Game.Faction, InvestmentTile>();

    public static UnityEvent<Game.Faction, Game.ActionType, int> 
        majorActionEvent = new UnityEvent<Game.Faction, Game.ActionType, int>(),
        minorActionEvent = new UnityEvent<Game.Faction, Game.ActionType, int>();

    public static UnityEvent<Map, AwardTile> 
        setAwardTile = new UnityEvent<Map, AwardTile>();

    public static UnityEvent 
        revealGlobalDemand = new UnityEvent(),
        revealMapAwards = new UnityEvent(),
        revealInvestmentTiles = new UnityEvent(); 

    public static UnityEvent<Game.Faction, Squadron> 
        constructSquadron = new UnityEvent<Game.Faction, Squadron>(),
        destroySquadron = new UnityEvent<Game.Faction, Squadron>();

    public static UnityEvent<Game.Faction, Space> 
        shiftSpace = new UnityEvent<Game.Faction, Space>(); 

}
