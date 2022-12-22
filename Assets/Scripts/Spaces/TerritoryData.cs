using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class TerritoryData : SpaceData
    {
        [field: SerializeField] public List<SpaceData> ConquestLines { get; private set; }
        [field: SerializeField] public bool PrestigeSpace { get; private set; }
    }
}