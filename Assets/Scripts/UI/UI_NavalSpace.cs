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
    [SerializeField] TextMeshProUGUI spaceName;
    [SerializeField] Image trim, highlight, background;
    public NavalSpace navalSpace; 

    private void Awake()
    {
        GetComponent<NavalSpace>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent += Style;
        Game.NavalSpaces.Add(navalSpace, this);
        Game.Spaces.Add(navalSpace, this); 
    }

    [Button]
    public override void Style()
    {
        GraphicSettings settings = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = navalSpace.name;
        trim.color = navalSpace.prestige ? settings.prestigeHighlightColor : Color.white; 
    }
}
