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
        protected override Space Space => territory; 
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
            spaceName.text = Space.Name;

            if ((Space as Territory).Prestigious)
            {
                trim.color = graphics.prestigeHighlightColor;
                flagCost.color = Color.white;
            }
            else
            {
                trim.color = Color.white;
                flagCost.color = Color.black;
            }

            background.color = Space.Flag.Color;
            spaceName.color = Space.Flag == Game.Neutral || Space.Flag == Game.Spain ? Color.black : Color.white;
        }
    }
}