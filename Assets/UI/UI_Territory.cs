using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UI_Territory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spaceName, flagCost;
    [SerializeField] Image territoryFrame, background;

    [Button]
    public void Style(Territory territory)
    {
        spaceName.text = territory.name;
        flagCost.text = territory.flagCost.ToString();

        background.color = FindObjectOfType<Game>().graphicSettings.factionColors[territory.flag];
        territoryFrame.color = territory.conflictMarker ? Color.red : Color.white;
    }
}
