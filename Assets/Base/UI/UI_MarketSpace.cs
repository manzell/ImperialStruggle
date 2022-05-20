using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_MarketSpace : MonoBehaviour
{
    public Market market;
    [SerializeField] Image background, highlight, trim;
    [SerializeField] TextMeshProUGUI marketName, flagCost;
    [SerializeField] GameObject marketCircle; 

    public void Awake()
    {
        market.updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style);
    }

    void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        marketName.text = market.name;
        flagCost.text = market.flagCost.ToString();

        marketCircle.GetComponent<Image>().color = graphics.resourceColors[market.marketType];
        marketCircle.GetComponentInChildren<Image>().sprite = graphics.resourceSprites[market.marketType];
        background.color = graphics.factionColors[market.flag];

    }
}
