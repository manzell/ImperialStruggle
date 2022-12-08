using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_PoliticalSpace : UI_Space
{
    PoliticalSpace politicalSpace;
    [SerializeField] PoliticalData politicalData;

    [SerializeField] TextMeshProUGUI spaceName, flagCost;
    [SerializeField] Image trim, highlight, background;
    [SerializeField] Color trimColor = new Color(235, 215, 171);

    private void Awake()
    {
        Game.startGameEvent += Style;
    }

    [Button]
    public override void Style()
    {
        if (politicalSpace == null)
        {
            politicalSpace = (PoliticalSpace)Game.SpaceLookup[politicalData];
            politicalSpace.updateSpaceEvent += Style;
        }

        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = politicalSpace.name;
        flagCost.text = politicalSpace.FlagCost.ToString();
        trim.color = politicalSpace.Prestigious ? graphics.prestigeHighlightColor : trimColor; 
        highlight.gameObject.SetActive(politicalSpace.conflictMarker);
        background.color = graphics.factionColors[politicalSpace.Flag];
        spaceName.color = politicalSpace.Flag == null || politicalSpace.Flag == Game.Spain ? Color.black : Color.white; 
    }
}
