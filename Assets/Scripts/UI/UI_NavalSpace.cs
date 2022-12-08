using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Sirenix.OdinInspector;
using DG.Tweening; 

public class UI_NavalSpace : UI_Space
{
    NavalSpace navalSpace;
    [SerializeField] NavalData navalData; 
    [SerializeField] TextMeshProUGUI spaceName;
    [SerializeField] Image trim, highlight, background;
    
    private void Awake()
    {
        Game.startGameEvent += Style;
    }

    [Button]
    public override void Style()
    {
        if (navalSpace == null)
        {
            navalSpace = (NavalSpace)Game.SpaceLookup[navalData];
            navalSpace.updateSpaceEvent += Style;
        }
        GraphicSettings settings = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = navalSpace.name;
        trim.color = navalSpace.Prestigious ? settings.prestigeHighlightColor : Color.white; 
    }
}
