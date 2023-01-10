using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class AwardTile : SerializedScriptableObject
    {
        public ActionPoints ActionPoints;
        public readonly int RequiredMargin;
    }
}