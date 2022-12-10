using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_Fort : UI_Space
    {
        Fort fort;
        [SerializeField] FortData fortData;
        [SerializeField] Image background, highlight, trim;
        [SerializeField] TextMeshProUGUI fortName, flagCost;

        private void Awake()
        {
            Game.startGameEvent += Style;
        }

        [Button]
        public override void Style()
        {
            if (fort == null)
            {
                fort = (Fort)Game.SpaceLookup[fortData];
                fort.updateSpaceEvent += Style;
            }
            GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

            fortName.text = fort.name;
            flagCost.text = fort.FlagCost.ToString();
            background.color = graphics.factionColors[fort.Flag];
            fortName.color = fort.Flag == null ? Color.black : Color.white;

        }
    }
}