using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class Player : SerializedMonoBehaviour, ISelectable
{
    public Game.Faction faction; 
    public List<EventCard> hand;
    public List<MinistryCard> ministers; // bool = revealed?
    public ActionPoints actionPoints = new ActionPoints(); 
    public List<WarTile> basicWarTiles, bonusWarTiles;
    public int CP = 0; 

    public static Dictionary<Game.Faction, Player> players = new Dictionary<Game.Faction, Player>();

    public List<Game.Keyword> keywords
    {
        get
        {
            List<Game.Keyword> _keywords = new List<Game.Keyword>();
            foreach (MinistryCard card in ministers)
                foreach(Game.Keyword _keyword in card.keywords)                    
                    if(!_keywords.Contains(_keyword))
                        _keywords.Add(_keyword);

            return _keywords;
        }
    }

    private void Awake()
    {
        players.Add(faction, this); 
    }
}
