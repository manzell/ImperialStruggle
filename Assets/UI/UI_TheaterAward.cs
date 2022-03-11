using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UI_TheaterAward : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] TextMeshProUGUI mainText;

    private void Awake()
    {
        AwardPhase.SetMapAwardEvent.AddListener((map, tile) => { if (this.map == map) SetAwardText(tile); }); 
    }

    void SetAwardText(AwardTile tile)
    {
        mainText.text = tile.victoryPoints.ToString(); 
    }
}
