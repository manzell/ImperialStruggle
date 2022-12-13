using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class MinistryCardData : CardData, ISelectable
    {
        public string Name => name; 
        public Faction faction;
        [TextArea] public string cardText;
    }
}