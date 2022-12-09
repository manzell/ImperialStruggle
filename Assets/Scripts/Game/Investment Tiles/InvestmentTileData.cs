using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class InvestmentTileData : SerializedScriptableObject
    {
        public System.Action<Player> SelectEvent;

        public ActionPoint majorActionPoint;
        public ActionPoint minorActionPoint; 

        // Notes - I could just add CardActions (the new standard for Scriptable Actions?) here and execute them OnSelection? 
        public List<CardAction> cardActions; 

        public bool EventTrigger;
        public bool MilUpgrade; 
    }
}
