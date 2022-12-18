using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_PoliticalSpace : UI_Space
    {
        PoliticalSpace space;
        protected override Space Space => space; 
        [SerializeField] PoliticalData politicalData;
        [SerializeField] TextMeshProUGUI flagCost;
        [SerializeField] Color trimColor = new Color(235, 215, 171);

        void Start()
        {
            space = (PoliticalSpace)Game.SpaceLookup[politicalData];
            space.updateSpaceEvent += Style;
        }

        [Button] public override void Style()
        {
            Debug.Log("Style"); 
            GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

            spaceName.text = space.Name;
            flagCost.text = space.FlagCost.ToString();
            trim.color = space.Prestigious ? graphics.prestigeHighlightColor : trimColor;
            highlight.gameObject.SetActive(space.conflictMarker);
            background.color = space.Flag.Color;
            spaceName.color = space.Flag == null || space.Flag == Game.Spain ? Color.black : Color.white;
        }
    }
}