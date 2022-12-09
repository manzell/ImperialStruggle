using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class CardData : SerializedScriptableObject
    {
        public HashSet<Phase.Era> eras;
        public List<MinistryCard.Keyword> keywords = new();

    }
}
