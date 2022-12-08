using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class EventCardData : SerializedScriptableObject
{
    public Phase.Era era; 
    public List<MinistryCard.Keyword> keywords = new ();
    public ActionPoint.ActionType reqdActionType;

    public MinistryCard.Keyword bonusKeyword;
    public string bonusCondition;
    public Dictionary<Faction, (string baseText, string bonusText)> factionText;
}
