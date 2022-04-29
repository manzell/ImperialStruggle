using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector; 

public class Theater : SerializedMonoBehaviour
{
    public Map map;
    public Dictionary<Game.Faction, List<WarTile>> warTiles; 
    public List<ScoringBonus> scoringBonuses; 
    public List<TheaterAwards> theaterAwards;
    public List<Territory> availableTerritories;

    public Game.Faction winningFaction;

    public Dictionary<Game.Faction, int> theaterScore => new Dictionary<Game.Faction, int>() {
        { Game.Faction.Britain, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.Faction.Britain).Count()
            +  warTiles[Game.Faction.Britain].Sum(tile => tile.value) },
        { Game.Faction.France, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.Faction.France).Count()
            +  warTiles[Game.Faction.France].Sum(tile => tile.value) }
    };
}
