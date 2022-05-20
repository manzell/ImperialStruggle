using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class PeaceTurn : SerializedMonoBehaviour
{
    public Player initiative; 
    public Dictionary<InvestmentTile, Game.Faction> investmentTiles = new Dictionary<InvestmentTile, Game.Faction>(); 
    public List<Game.Resource> globalDemandResources = new List<Game.Resource>();
    public Dictionary<Map, AwardTile> awardTiles = new Dictionary<Map, AwardTile>();  
}
