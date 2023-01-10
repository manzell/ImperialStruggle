using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class AdvantageData : SerializedScriptableObject, ISelectable
    {
        public Action UISelectionEvent { get; set; }
        public Action UIDeselectEvent { get; set; }

        public string Name => name;
        public HashSet<SpaceData> adjacentSpaces;
        public List<IPlayerAction> playerActions; 
    }
}
