using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_MarketSpace : UI_Space
    {
        Market market;
        protected override Space Space => market;
        [SerializeField] MarketData marketData;
        [SerializeField] Image resourceIcon, resourceBackground;
        [SerializeField] TextMeshProUGUI flagCost;
        [SerializeField] GameObject marketCircle;

        void Start()
        {
            market = (Market)Game.SpaceLookup[marketData];
            market.updateSpaceEvent += Style;
        }

        [Button]
        public override void Style()
        {
            spaceName.text = market.Name;
            flagCost.text = market.FlagCost.ToString();

            if(market.Resource != null)
            {
                resourceBackground.color = market.Resource.resourceColor;
                resourceIcon.sprite = market.Resource.resourceIcon;
            }

            background.color = market.Flag.Color; 
            spaceName.color = market.Flag == null || market.Flag == Game.Spain ? Color.black : Color.white;
        }
    }
}