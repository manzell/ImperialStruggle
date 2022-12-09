using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector; 

public class GraphicSettings : SerializedScriptableObject
{
    public Dictionary<Faction, Sprite> flags;
    public Dictionary<MinistryCard.Keyword, Sprite> keywordIcons;
    public Dictionary<ActionPoint.ActionType, Sprite> actionIcons;
    public Dictionary<Faction, Color> factionColors;
    public GameObject PopupMenu, PopupAction; 
    public Color prestigeHighlightColor;
}
