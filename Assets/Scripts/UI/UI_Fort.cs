using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_Fort : UI_Space
{
    public Fort fort;
    public Image background, highlight, trim;
    public TextMeshProUGUI fortName, flagCost;

    private void Awake()
    {
        GetComponent<Fort>().updateSpaceEvent.AddListener(Style);
        Game.startGameEvent += Style;
        Game.Forts.Add(fort, this);
        Game.Spaces.Add(fort, this); 
    }

    [Button] public override void Style()
    {
        GraphicSettings graphics = FindObjectOfType<Game>().graphicSettings; 
        fort = GetComponent<Fort>();

        fortName.text = fort.name;
        flagCost.text = fort.flagCost.ToString();
        background.color = graphics.factionColors[fort.flag];
        fortName.color = fort.flag == null ? Color.black : Color.white; 

    }
}
