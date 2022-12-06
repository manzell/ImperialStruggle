using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_MarketSpace : UI_Space
{
    public Market market;
    [SerializeField] Image background, highlight, trim, resourceIcon, resourceBackground;
    [SerializeField] TextMeshProUGUI marketName, flagCost;
    [SerializeField] GameObject marketCircle; 

    public void Awake()
    {
        market.updateSpaceEvent.AddListener(Style);
        Game.startGameEvent += Style;
        Game.Markets.Add(market, this);
        Game.Spaces.Add(market, this);
    }

    [Button]
    public override void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        marketName.text = market.name;
        flagCost.text = market.flagCost.ToString();

        resourceBackground.color = market.marketType.resourceColor;
        resourceIcon.sprite = market.marketType.resourceIcon.sprite; 
        background.color = graphics.factionColors[market.flag];
        marketName.color = market.flag == null || market.flag == Game.Spain ? Color.black : Color.white;
    }
}
