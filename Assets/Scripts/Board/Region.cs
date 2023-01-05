using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class Region : ScriptableObject, ISelectable
    {
        public string Name => name;
        [field:SerializeField] public Map map { get; private set; }
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }
    }
}