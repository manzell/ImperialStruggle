using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public abstract class SpaceData : SerializedScriptableObject
    {
        [field: SerializeField] public GameObject prefab { get; private set; }
        [field: SerializeField] public Map Map { get; private set; }
        [field: SerializeField] public Region Region { get; private set; }
        [field: SerializeField] public Faction startingFlag { get; private set; }
        [field: SerializeField] public Phase.Era availableEra { get; private set; }
        [field: SerializeField] public List<SpaceData> adjacentSpaces { get; private set; }
    }
}