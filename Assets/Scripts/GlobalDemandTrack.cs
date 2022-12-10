using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public class GlobalDemandTrack : SerializedScriptableObject
    {
        public Dictionary<GlobalDemandKey, ActionPoints> GlobalDemandAwards;
        public HashSet<Resource> Resources => new(GlobalDemandAwards.Select(award => award.Key.Resource));
    }

    public struct GlobalDemandKey
    {
        public Phase.Era Era;
        public Resource Resource;
    }
}