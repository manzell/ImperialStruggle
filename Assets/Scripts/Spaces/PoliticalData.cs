using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class PoliticalData : SpaceData
    {
        [field: SerializeField] public int FlagCost { get; private set; }
        [field: SerializeField] public bool AllianceSpace { get; private set; }
        [field: SerializeField] public bool PrestigeSpace { get; private set; }
    }
}