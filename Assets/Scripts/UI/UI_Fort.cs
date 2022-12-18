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
        protected override Space Space => fort;
        [SerializeField] FortData fortData;
        [SerializeField] TextMeshProUGUI flagCost;

        void Start()
        {
            fort = (Fort)Game.SpaceLookup[fortData];
            fort.updateSpaceEvent += Style;
        }

        [Button] public override void Style()
        {
            spaceName.text = Space.Name;
            flagCost.text = (Space as Fort).FlagCost.ToString();
            background.color = Space.Flag.Color;
            spaceName.color = Space.Flag == null ? Color.black : Color.white;
        }
    }
}