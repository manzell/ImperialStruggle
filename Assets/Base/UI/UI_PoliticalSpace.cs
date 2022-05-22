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
    [SerializeField] Color trimColor = new Color(235, 215, 171);
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
        trim.color = space.prestige ? graphics.prestigeHighlightColor : trimColor; 
        highlight.gameObject.SetActive(space.conflictMarker);
        background.color = graphics.factionColors[space.flag];
        spaceName.color = space.flag == Game.Faction.Neutral || space.flag == Game.Faction.Spain ? Color.black : Color.white; 
    }
}
