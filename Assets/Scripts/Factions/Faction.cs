using ImperialStruggle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI; 

public class Faction : ScriptableObject, ISelectable
{
    public Sprite Icon;
    public Color Color;
    public List<MinistryCardData> ministers;
    public List<WarTile> basicWarTiles, advancedWarTiles;
    public string Name => name;

    public Player player; 
}
