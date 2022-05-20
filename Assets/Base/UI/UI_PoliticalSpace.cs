using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_PoliticalSpace : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spaceName, flagCost;
    [SerializeField] Image trim, highlight, background;
    public PoliticalSpace space; 

    private void Awake()
    {
        space.updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style);
    }

    [Button]
    void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings;

        spaceName.text = space.name;
        flagCost.text = space.flagCost.ToString();
        trim.color = space.prestige ? graphics.prestigeHighlightColor : Color.white; 
        highlight.gameObject.SetActive(space.conflictMarker);
        background.color = graphics.factionColors[space.flag];
    }
}
