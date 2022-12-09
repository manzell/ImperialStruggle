using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class MinistryCardData : CardData
    {
        public Faction faction;
        [TextArea] public string cardText;
    }
}