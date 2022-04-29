using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarTile : MonoBehaviour
{
    public int value;
    public bool debt, milDamage, unflag; 
    public Game.Faction faction;
    public Game.Faction opposingFaction => faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;
}
