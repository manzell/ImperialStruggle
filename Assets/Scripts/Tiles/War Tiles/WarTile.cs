using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector; 

namespace ImperialStruggle
{
    public class WarTile : SerializedScriptableObject, ISelectable
    {
        public enum WarTileSet { Basic, Bonus }
        public string Name => $"{faction.Name} {warTileSet}: {(value >= 0 ? "+" : string.Empty)}{value} " +
                    $"{(actions.Count > 0 ? actions.First().ToString().Substring(0, 3) : string.Empty)}";

        public string subname;
        public Faction faction;
        public WarTileSet warTileSet;
        public int value;

        [SerializeField] Queue<PlayerAction> actions;
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
    }
}