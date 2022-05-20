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
        GraphicSettings graphicSettings = FindObjectOfType<Game>().graphicSettings;
        this.tile = tile;        

        foreach(ActionPoint actionPoint in tile.actionPoints)
        {
            if(actionPoint.actionTier == ActionPoint.ActionTier.Major)
            {
                majorActionPoints.text = actionPoint.Value(null).ToString(); 
                majorIcon.sprite = graphicSettings.actionIcons[actionPoint.actionType];
            }
            else if (actionPoint.actionTier == ActionPoint.ActionTier.Minor)
            {
                minorActionPoints.text = actionPoint.Value(null).ToString();
                minorIcon.sprite = graphicSettings.actionIcons[actionPoint.actionType];
            }
        }

        eventIcon.enabled = tile.GetComponent<PlayEventCardAction>();
        milUpgradeIcon.enabled = tile.GetComponent<MilitaryUpgradeAction>(); 
    }
}
