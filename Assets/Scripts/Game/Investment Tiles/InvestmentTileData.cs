using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class InvestmentTile : SerializedScriptableObject, ISelectable
    {
        public string Name => $"{majorActionPoint.baseValue} {majorActionPoint.actionType}/{minorActionPoint.actionType}" +
            $"{(MilUpgrade ? "Mil" : string.Empty)}{(EventTrigger ? "Event" : string.Empty)}"; 

        public System.Action<Player> SelectEvent;

        public ActionPoint majorActionPoint;
        public ActionPoint minorActionPoint; 

        // Notes - I could just add CardActions (the new standard for Scriptable Actions?) here and execute them OnSelection? 
        public List<CardAction> cardActions; 

        public bool EventTrigger;
        public bool MilUpgrade; 
    }
}
