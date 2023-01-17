using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class AdvantageData : SerializedScriptableObject, ISelectable
    {
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public string Name => name;
        public HashSet<SpaceData> adjacentSpaces;
        public List<PlayerAction> playerActions; 
    }
}
