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
        protected override Space space => territory; 
        [SerializeField] TerritoryData territoryData;
        [SerializeField] TextMeshProUGUI flagCost;

        void Start()
        {
            territory = (Territory)Game.SpaceLookup[territoryData];
            territory.updateSpaceEvent += Style;
        }

        [Button]
        public override void Style()
        {
            GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;
            spaceName.text = space.Name;
            background.color = space.Flag.Color;
            spaceName.color = space.Flag == Game.Neutral || space.Flag == Game.Spain ? Color.black : Color.white;

            if ((space as Territory).Prestigious)
            {
                trim.color = graphics.prestigeHighlightColor;
                flagCost.color = Color.white;
            }
            else
            {
                trim.color = Color.white;
                flagCost.color = Color.black;
            }
        }
    }
}