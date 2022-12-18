using ImperialStruggle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI; 

public class Faction : ScriptableObject, ISelectable
{
    public Sprite Icon, Flag;
    public Color Color;
    public List<MinistryCardData> ministers;
    public List<WarTile> basicWarTiles, advancedWarTiles;
    public string Name => name;

    public Player player;
    public Action UISelectionEvent { get; set; }
    public Action UIDeselectEvent { get; set; }
}
