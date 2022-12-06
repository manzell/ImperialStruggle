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
    [SerializeField] Color trimColor = new Color(235, 215, 171);
    public PoliticalSpace politicalSpace; 

    private void Awake()
    {
        politicalSpace.updateSpaceEvent.AddListener(Style);
        Game.startGameEvent += Style;
        Game.PoliticalSpaces.Add(politicalSpace, this);
        Game.Spaces.Add(politicalSpace, this);
    }

    [Button]
    public override void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = politicalSpace.name;
        flagCost.text = politicalSpace.flagCost.ToString();
        trim.color = politicalSpace.prestige ? graphics.prestigeHighlightColor : trimColor; 
        highlight.gameObject.SetActive(politicalSpace.conflictMarker);
        background.color = graphics.factionColors[politicalSpace.flag];
        spaceName.color = politicalSpace.flag == null || politicalSpace.flag == Game.Spain ? Color.black : Color.white; 
    }
}
