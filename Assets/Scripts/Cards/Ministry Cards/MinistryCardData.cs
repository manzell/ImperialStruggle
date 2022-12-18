using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class MinistryCardData : CardData, ISelectable
    {
        public string Name => name;
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
        public Faction faction;
        [TextArea] public string cardText;
        [TextArea] public string flavor; 
    }
}