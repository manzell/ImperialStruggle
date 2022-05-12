using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarTile : MonoBehaviour
{
    public enum WarTileSet { Basic, Bonus }
    public int value;
    public WarTileSet warTileSet;
    public bool debt, milDamage, unflag; 
    public Game.Faction faction;
    public Game.Faction opposingFaction
    {
        get
        {
            if (faction == Game.Faction.Britain) return Game.Faction.France;
            else if (faction == Game.Faction.France) return Game.Faction.Britain;
            else return Game.Faction.Neutral;
        }
    }
}
