using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_Fort : MonoBehaviour
{
    public Fort fort;
    public Image background, highlight, trim;
    public TextMeshProUGUI fortName, flagCost;

    private void Awake()
    {
        GetComponent<Fort>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent.AddListener(Style);
    }
    [Button]
    void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings; 
        fort = GetComponent<Fort>();

        fortName.text = fort.name;
        flagCost.text = fort.flagCost.ToString();
        background.color = graphics.factionColors[fort.flag];
        fortName.color = fort.flag == Game.Faction.Neutral ? Color.black : Color.white; 

    }
}
