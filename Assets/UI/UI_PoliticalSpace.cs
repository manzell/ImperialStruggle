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
    //PoliticalSpace space; 

    private void Awake()
    {
        space = GetComponent<PoliticalSpace>();
        Style();

        ShiftSpace.shiftSpaceEvent.AddListener(ss => { if (ss.space == space) Style(); });
    }

    public void Style()
    {
        spaceName.text = space.name;
        flagCost.text = space.flagCost.ToString();

        trim.gameObject.SetActive(space.prestige);
        highlight.gameObject.SetActive(space.conflictMarker);
        background.color = FindObjectOfType<Game>().graphicSettings.factionColors[space.flag];
    }
}
