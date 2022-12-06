using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq; 

public class UI_TheaterAward : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] TextMeshProUGUI mainText;

    private void Awake()
    {
        //AwardPhase.SetMapAwardEvent.AddListener((map, tile) => { if (this.map == map) SetAwardText(tile); }); 
    }

    void SetAwardText(AwardTile tile)
    {
        //AdjustVPCommand vpCommand = tile.GetComponent<AdjustVPCommand>();
        //AdjustTPCommand tpCommand = tile.GetComponent<AdjustTPCommand>();

        //if (vpCommand != null) // TODO put this in the graphics manifold
        //    mainText.text = $"<color = \"black\">{vpCommand.adjustAmount}</color>";
        //if (vpCommand != null && tpCommand != null)
        //    mainText.text += " ";
        //if (tpCommand != null)
        //    mainText.text += $"<color = \"green\">{tpCommand.adjustAmount}</color>";
    }
}
