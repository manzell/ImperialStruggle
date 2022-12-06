using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarTile : MonoBehaviour, ISelectable
{
    public string subname; 
    public enum WarTileSet { Basic, Bonus }
    public int value;
    public WarTileSet warTileSet;
    public List<GameAction> actions;
    public List<Command> commands; 
    public bool debt, milDamage, unflag;
    public Faction faction;

    public string tileName => $"{warTileSet}: {value} {(debt ? "D " : "")}{(milDamage ? "M " : "")}{(unflag ? "U " : "")}";
}
