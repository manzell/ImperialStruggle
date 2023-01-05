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
        protected override Space space => fort;
        [SerializeField] FortData fortData;
        [SerializeField] TextMeshProUGUI flagCost;

        void Start()
        {
            fort = (Fort)Game.SpaceLookup[fortData];
            fort.updateSpaceEvent += Style;
        }

        [Button] public override void Style()
        {
            spaceName.text = space.Name;
            flagCost.text = (space as Fort).flagCost.GetAPCost(Game.ActivePlayer, fort).Value(null).ToString();
            background.color = space.Flag.Color;
            spaceName.color = space.Flag == null ? Color.black : Color.white;
        }
    }
}