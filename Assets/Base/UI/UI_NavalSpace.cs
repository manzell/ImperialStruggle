using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Sirenix.OdinInspector;
using DG.Tweening; 

public class UI_NavalSpace : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spaceName;
    [SerializeField] Image trim, highlight, background;
    public NavalSpace navalSpace; 

    private void Awake()
    {
        GetComponent<NavalSpace>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style); 
    }

    [Button]
    public void Style()
    {
        GraphicSettings settings = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = navalSpace.name;
        trim.color = navalSpace.prestige ? settings.prestigeHighlightColor : Color.white; 
    }
}
