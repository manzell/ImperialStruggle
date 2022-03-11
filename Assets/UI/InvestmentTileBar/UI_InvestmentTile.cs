using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Sirenix.OdinInspector; 

public class UI_InvestmentTile : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI majorActionPoints, minorActionPoints;
    [SerializeField] Image majorIcon, minorIcon, eventIcon, milUpgradeIcon;
    public InvestmentTile tile;

    private void Awake()
    {
        SelectInvestmentTile.selectInvestmentTileEvent.AddListener(t => {if (t == tile) Style(t); }); 
    }

    [Button]
    public void SetTile(InvestmentTile tile)
    {
        this.tile = tile;
        Style(tile); 
    }

    public void Style(InvestmentTile tile)
    {
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;

        majorActionPoints.text = tile.majorAction.First().Value.ToString();
        minorActionPoints.text = tile.minorAction.First().Value.ToString();
        majorIcon.sprite = graphicSettings.actionIcons[tile.majorAction.First().Key];
        minorIcon.sprite = graphicSettings.actionIcons[tile.minorAction.First().Key];
        eventIcon.enabled = tile.eventCardTrigger;
        milUpgradeIcon.enabled = tile.milUpgradeTrigger;

        // OK how to tell if the investment tile is selected? TODO - This is very ineffecient.
        if(Phase.currentPhase is ActionRound)
        {
            foreach(ActionRound actionRound in Phase.currentPhase.transform.parent.GetComponentsInChildren<ActionRound>())
            {
                if(actionRound.investmentTile == tile)
                {
                    if(actionRound.actingFaction == Game.Faction.Neutral)
                    {
                        GetComponent<Image>().color = graphicSettings.factionColors[actionRound.actingFaction];
                        GetComponent<Outline>().effectColor = new Color(226, 217, 144);
                    }
                    else
                    {
                        GetComponent<Image>().color = graphicSettings.factionColors[actionRound.actingFaction];
                        GetComponent<Outline>().effectColor = Color.white;
                    }
                }
            }
        }
    }
}
