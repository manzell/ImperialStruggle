using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class Player : SerializedMonoBehaviour
{
    public Game.Faction faction; 
    public List<EventCard> hand;
    public List<MinistryCard> ministers; // bool = revealed?
    public Dictionary<(Game.ActionType, Game.ActionTier), int> actionPoints = new Dictionary<(Game.ActionType, Game.ActionTier), int>(); 
    public List<WarTile> basicWarTiles, bonusWarTiles;
    public int CP = 0; 

    public static Dictionary<Game.Faction, Player> players = new Dictionary<Game.Faction, Player>();

    private void Awake()
    {
        players.Add(faction, this); 
    }
}
