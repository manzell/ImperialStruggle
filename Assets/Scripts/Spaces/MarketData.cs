using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MarketData : SpaceData
    { 
        [field: SerializeField] public Resource ResourceType { get; private set; }
        [field: SerializeField] public int FlagCost { get; private set; }
    }
}