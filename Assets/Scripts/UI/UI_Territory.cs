using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_Territory : UI_Space
    {
        Territory territory;
        [SerializeField] TerritoryData territoryData;
        [SerializeField] TextMeshProUGUI spaceName, flagCost;
        [SerializeField] Image territoryFrame, background;

        private void Awake()
        {
            Game.startGameEvent += Style;
        }

        [Button]
        public override void Style()
        {
            if (territory == null)
            {
                territory = (Territory)Game.SpaceLookup[territoryData];
                territory.updateSpaceEvent += Style;
            }

            GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;
            spaceName.text = territory.name;


            if (territory.Prestigious)
            {
                territoryFrame.color = graphics.prestigeHighlightColor;
                flagCost.color = Color.white;
            }
            else
            {
                territoryFrame.color = Color.white;
                flagCost.color = Color.black;
            }

            background.color = graphics.factionColors[territory.Flag];
            spaceName.color = territory.Flag == Game.Neutral || territory.Flag == Game.Spain ? Color.black : Color.white;
        }
    }
}