using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Theater : Phase
{
    public Map map;
    public List<WarTile> warTiles, scoredWarTiles;
    public List<ScoringBonus> scoringBonuses; 
    public List<TheaterAwards> theaterAwards;
    public List<Territory> availableTerritories;

    public Game.Faction winningFaction;

    public Dictionary<Game.Faction, int> theaterScore
    {
        get
        {
            return new Dictionary<Game.Faction, int>() { 
                { Game.Faction.Britain, scoringBonuses.Where(bonus => 
                    bonus.scoringFaction == Game.Faction.Britain).Count() + warTiles.Where(tile => tile.faction == Game.Faction.Britain).Sum(tile => tile.value) }, 
                { Game.Faction.France, scoringBonuses.Where(bonus => 
                    bonus.scoringFaction == Game.Faction.France).Count() + warTiles.Where(tile => tile.faction == Game.Faction.Britain).Sum(tile => tile.value) }
            };
        }
    }
}
