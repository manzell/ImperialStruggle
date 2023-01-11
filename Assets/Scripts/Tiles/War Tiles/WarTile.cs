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
                    $"{(Actions.Count > 0 ? Actions.First().ToString().Substring(0, 3) : string.Empty)}";

        public Dictionary<Phase.Era, string> subname { get; private set; }
        public Faction faction { get;  private set; }
        public WarTileSet warTileSet { get; private set; }
        public int value { get; private set; }

        [field:SerializeField] public Queue<PlayerAction> Actions { get; private set; }
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
    }
}