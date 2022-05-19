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

    private void Awake()
    {
        Style();
        GetComponent<Space>().updateSpaceEvent.AddListener(Style);
    }

    [Button]
    public void Style()
    {
        Space space = GetComponent<NavalSpace>();
        spaceName.text = space.name;
        trim.DOColor(space.prestige ? FindObjectOfType<Game>().graphicSettings.prestigeHighlightColor : Color.white, 0.25f);
        highlight.DOFade(space.conflictMarker ? 0.4f : 0f, 0.25f); 
        background.DOColor(FindObjectOfType<Game>().graphicSettings.factionColors[space.flag], 0.25f);
    }
}
