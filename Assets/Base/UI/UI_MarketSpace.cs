using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 
public class UI_MarketSpace : MonoBehaviour
{
    public Market market;
    [SerializeField] Image background, highlight, trim, resourceIcon, resourceBackground;
    [SerializeField] TextMeshProUGUI marketName, flagCost;
    [SerializeField] GameObject marketCircle; 

    public void Awake()
    {
        market.updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style);
    }

    [Button]
    void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        marketName.text = market.name;
        flagCost.text = market.flagCost.ToString();

        resourceBackground.color = graphics.resourceColors[market.marketType];
        resourceIcon.sprite = graphics.resourceSprites[market.marketType];
        background.color = graphics.factionColors[market.flag];
        marketName.color = market.flag == Game.Faction.Neutral || market.flag == Game.Faction.Spain ? Color.black : Color.white;
    }
}
