using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class InvestmentTile : SerializedScriptableObject, ISelectable
    {
        public string Name => $"{majorActionPoint.baseValue} {majorActionPoint.actionType.ToString().Substring(0,3)}/{minorActionPoint.actionType.ToString().Substring(0,3)}" +
            $"{(actions.Any(a => a is MilitaryUpgradeAction) ? " Mil" : string.Empty)}{(actions.Any(a => a is TriggerEventAction)? " Event" : string.Empty)}"; 

        public System.Action<Player> SelectEvent;

        public ActionPoint majorActionPoint;
        public ActionPoint minorActionPoint; 

        // Notes - I could just add CardActions (the new standard for Scriptable Actions?) here and execute them OnSelection? 
        public List<PlayerAction> actions; 
    }
}
