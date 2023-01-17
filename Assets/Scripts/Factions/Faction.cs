using ImperialStruggle;
using System.Collections.Generic;
using UnityEngine;

public class Faction : ScriptableObject, ISelectable
{
    public Sprite Icon, Flag;
    public Color Color;
    public List<MinistryCardData> ministers;
    public List<WarTile> basicWarTiles, advancedWarTiles;
    public string Name => name;

    public Player player;
    public System.Action UISelectionEvent { get; set; }
    public System.Action UIDeselectEvent { get; set; }
}
