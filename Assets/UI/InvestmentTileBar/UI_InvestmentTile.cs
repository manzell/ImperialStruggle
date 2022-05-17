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

    [Button]
    public void SetTile(InvestmentTile tile)
    {
        this.tile = tile;
        //Style(tile); 
    }

    //public void Style(InvestmentTile tile)
    //{
    //    GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;

    //    foreach(ActionPoint actionPoint in tile.GetComponent<AdjustAPCommand>().actionPoints)
    //    {
    //        if(actionPoint.actionTier == ActionPoint.ActionTier.Major)
    //        {
    //            majorActionPoints.text = actionPoint.Value(new List<ICriteria>()).ToString(); 
    //            majorIcon.sprite = graphicSettings.actionIcons[actionPoint.actionType];
    //        }
    //        else if (actionPoint.actionTier == ActionPoint.ActionTier.Minor)
    //        {
    //            minorActionPoints.text = actionPoint.Value(new List<ICriteria>()).ToString();
    //            minorIcon.sprite = graphicSettings.actionIcons[actionPoint.actionType];
    //        }
    //    }

    //    //eventIcon.enabled = tile.GetComponent<EventCardTriggerCommand>();
    //    //milUpgradeIcon.enabled = tile.TryGetComponent<MilitaryUpgradeCommand>(out MilitaryUpgradeCommand m); 

    //    // OK how to tell if the investment tile is selected? TODO - This is very ineffecient.
    //    if(Phase.currentPhase is ActionRound)
    //    {
    //        foreach(ActionRound actionRound in Phase.currentPhase.transform.parent.GetComponentsInChildren<ActionRound>())
    //        {
    //            if(actionRound.investmentTile == tile)
    //            {
    //                if(actionRound.actingFaction == Game.Faction.Neutral)
    //                {
    //                    GetComponent<Image>().color = graphicSettings.factionColors[actionRound.actingFaction];
    //                    GetComponent<Outline>().effectColor = new Color(226, 217, 144);
    //                }
    //                else
    //                {
    //                    GetComponent<Image>().color = graphicSettings.factionColors[actionRound.actingFaction];
    //                    GetComponent<Outline>().effectColor = Color.white;
    //                }
    //            }
    //        }
    //    }
    //}
}
