using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UI_Territory : UI_Space
{
    [SerializeField] TextMeshProUGUI spaceName, flagCost;
    [SerializeField] Image territoryFrame, background;
    public Territory territory;

    private void Awake()
    {
        GetComponent<Territory>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent += Style;
        Game.Territories.Add(territory, this);
        Game.Spaces.Add(territory, this);
    }

    [Button]
    public override void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;
        spaceName.text = territory.name;
        flagCost.text = territory.flagCost.ToString();

        if(territory.prestige)
        {
            territoryFrame.color = graphics.prestigeHighlightColor;
            flagCost.color = Color.white; 
        }
        else
        {
            territoryFrame.color = Color.white;
            flagCost.color = Color.black;
        }

        background.color = graphics.factionColors[territory.flag];
        spaceName.color = territory.flag == Game.Neutral || territory.flag == Game.Spain ? Color.black : Color.white; 
    }
}
