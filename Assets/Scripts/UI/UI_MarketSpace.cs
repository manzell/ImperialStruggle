using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_MarketSpace : UI_Space
{
    Market market;
    [SerializeField] MarketData marketData; 
    [SerializeField] Image background, highlight, trim, resourceIcon, resourceBackground;
    [SerializeField] TextMeshProUGUI marketName, flagCost;
    [SerializeField] GameObject marketCircle; 

    public void Awake()
    {
        Game.startGameEvent += Style;
    }

    [Button]
    public override void Style()
    {
        if (market == null)
            market = (Market)Game.SpaceLookup[marketData];

        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        marketName.text = market.name;
        flagCost.text = market.FlagCost.ToString();

        resourceBackground.color = market.Resource.resourceColor;
        resourceIcon.sprite = market.Resource.resourceIcon; 
        background.color = graphics.factionColors[market.Flag];
        marketName.color = market.Flag == null || market.Flag == Game.Spain ? Color.black : Color.white;
    }
}
