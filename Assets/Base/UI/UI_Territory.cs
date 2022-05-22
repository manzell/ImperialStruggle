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
    public Territory territory;

    private void Awake()
    {
        GetComponent<Territory>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style);
    }

    [Button]
    public void Style()
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
        spaceName.color = territory.flag == Game.Faction.Neutral || territory.flag == Game.Faction.Spain ? Color.black : Color.white; 
    }
}
