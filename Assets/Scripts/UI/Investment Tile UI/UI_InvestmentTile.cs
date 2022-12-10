using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
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

            majorActionPoints.text = tile.majorActionPoint.Value(null).ToString();
            majorIcon.sprite = graphicSettings.actionIcons[tile.majorActionPoint.actionType];

            minorActionPoints.text = tile.minorActionPoint.Value(null).ToString();
            minorIcon.sprite = graphicSettings.actionIcons[tile.minorActionPoint.actionType];

            eventIcon.enabled = tile.EventTrigger;
            milUpgradeIcon.enabled = tile.MilUpgrade;
        }
    }
}