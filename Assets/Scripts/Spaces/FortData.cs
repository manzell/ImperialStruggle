using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class FortData : SpaceData
    {
        [field: SerializeField] public int FlagCost { get; private set; }
    }
}