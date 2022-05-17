using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarTile : MonoBehaviour, ISelectable
{
    public enum WarTileSet { Basic, Bonus }
    public int value;
    public WarTileSet warTileSet;
    public List<BaseAction> actions;
    public List<Command> commands; 
    public bool debt, milDamage, unflag;
    public Game.Faction faction;

    public string tileName => $"{warTileSet}: {value} {(debt ? "D " : "")}{(milDamage ? "M " : "")}{(unflag ? "U " : "")}";
}
