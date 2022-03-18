using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_PoliticalSpace : UI_Space
{
    [SerializeField] TextMeshProUGUI spaceName, flagCost;
    [SerializeField] Image trim, highlight, background;

    private void Awake()
    {
        Style(); 
    }

    public void Style()
    {
        if (TryGetComponent(out PoliticalSpace space))
        {
            spaceName.text = space.name;
            flagCost.text = space.flagCost.ToString();

            trim.gameObject.SetActive(space.prestige);
            highlight.gameObject.SetActive(space.conflictMarker);
            background.color = FindObjectOfType<Game>().graphicSettings.factionColors[space.flag];
        }
    }
}
