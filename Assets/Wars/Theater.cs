using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector; 

public class Theater : SerializedMonoBehaviour, ISelectable
{
    public Map map;
    public List<WarTile> warTiles = new(); 
    public List<ScoringBonus> scoringBonuses; 
    public List<TheaterAwards> theaterAwards;
    public List<Territory> availableTerritories;

    public Faction winningFaction;

    public Dictionary<Faction, int> theaterScore => new Dictionary<Faction, int>() {
        { Game.Britain, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.Britain).Count()
            +  warTiles.Where(tile => tile.faction == Game.Britain).Sum(tile => tile.value) },
        { Game.France, scoringBonuses.Where(bonus => bonus.scoringFaction == Game.France).Count()
            +  warTiles.Where(tile => tile.faction == Game.France).Sum(tile => tile.value) }
    };
}
