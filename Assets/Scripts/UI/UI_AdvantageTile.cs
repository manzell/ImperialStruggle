using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector; 

public class UI_AdvantageTile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image trim, background, exhaustion;
    [SerializeField] TextMeshProUGUI tileName; 
    [SerializeField] AdvantageTile advantageTile;

    private void Awake()
    {
        //throw new System.NotImplementedException(); 
        /*
        foreach(Space space in advantageTile.adjacentSpaces)
            space.updateSpaceEvent += Style; 
        */
    }

    [Button]
    public void Style()
    {
        GraphicSettings settings = FindObjectOfType<Game>().graphicSettings; 

        tileName.text = advantageTile.name;
        background.color = settings.factionColors[advantageTile.faction];
        exhaustion.gameObject.SetActive(advantageTile.tileState == AdvantageTile.AdvantageTileState.Exhaused); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAction playerAction = GetComponent<PlayerAction>();
        ActionRound actionRound = Phase.CurrentPhase.GetComponent<ActionRound>(); 

        if(playerAction != null && !(advantageTile.tileState == AdvantageTile.AdvantageTileState.Exhaused) && advantageTile.faction == actionRound.actingFaction)
        {
            //playerAction.actingPlayer = actionRound.actingFaction;
            playerAction.Execute();
        }
    }
}
