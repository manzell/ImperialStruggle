using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public class GlobalDemandTrack : SerializedScriptableObject
    {
        public Dictionary<GlobalDemandKey, GlobalDemandValue> GlobalDemandAwards;
    }
        public struct GlobalDemandKey
    {
        public Phase.Era Era;
        public Resource Resource;

        public GlobalDemandKey(Phase.Era era, Resource resource)
        {
            Era = era;
            Resource = resource; 
        }
    }

    public struct GlobalDemandValue
    {
        public int VP, TP, debt; 
    }
}