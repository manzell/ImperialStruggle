using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace ImperialStruggle
{
    public class UI_NavalSpace : UI_Space
    {
        NavalSpace navalSpace;
        protected override Space Space => navalSpace; 
        [SerializeField] NavalData navalData;

        void Start()
        {
            navalSpace = (NavalSpace)Game.SpaceLookup[navalData];
            navalSpace.updateSpaceEvent += Style;
        }

        [Button] public override void Style()
        {
            GraphicSettings settings = FindObjectOfType<Game>().graphicSettings;

            spaceName.text = navalSpace.Name;
            trim.color = (navalSpace as NavalSpace).Prestigious ? settings.prestigeHighlightColor : Color.white;
        }
    }
}