using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class EventCardData : SerializedScriptableObject
{
    public Game.Era era; 
    public List<Game.Keyword> keywords = new List<Game.Keyword>();
    public ActionPoint.ActionType reqdActionType;

    public Game.Keyword bonusKeyword;
    public string bonusCondition;
    public Dictionary<Faction, (string baseText, string bonusText)> factionText;
}
