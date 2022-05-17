using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using System;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_ActionPoints : SerializedMonoBehaviour
{
    [SerializeField] GameObject actionPointPrefab;
    [SerializeField] Dictionary<ActionPoint.ActionTier, GameObject> actionTiers = new Dictionary<ActionPoint.ActionTier, GameObject>(); 
    [SerializeField] Button takeDebtButton, activateTPbutton, reduceDebtButton; 
    Dictionary<(ActionPoint.ActionType, ActionPoint.ActionTier), UI_ActionPoint> APtiles = new Dictionary<(ActionPoint.ActionType, ActionPoint.ActionTier), UI_ActionPoint>();

    // This displays our current ActionPoints on a Bar; ordered by Category, then by Major/Minor, then by value.
    // Actions Points with a restriction will be grouped and displayed separately, there will be a popup 
    // that shows the player what the restriction is. 
    // Activated Treaty Points are essentially a form of ActionPoint

}