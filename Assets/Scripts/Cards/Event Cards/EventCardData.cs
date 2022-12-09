using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImperialStruggle;

[CreateAssetMenu]
public class EventCardData : CardData
{
    public ActionPoint.ActionType reqdActionType;
    public MinistryCard.Keyword bonusKeyword;
    public string bonusCondition;

    public Dictionary<Faction, (string baseText, string bonusText)> factionText;
}
