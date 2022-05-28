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
        foreach(Space space in advantageTile.adjacentSpaces)
            space.updateSpaceEvent.AddListener(Style); 
    }

    [Button]
    public void Style()
    {
        GraphicSettings settings = FindObjectOfType<Game>().graphicSettings; 

        tileName.text = advantageTile.name;
        background.color = settings.factionColors[advantageTile.faction];
        exhaustion.gameObject.SetActive(advantageTile.exhausted); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAction playerAction = GetComponent<PlayerAction>();
        ActionRound actionRound = Phase.currentPhase.GetComponent<ActionRound>(); 

        if(playerAction != null && !advantageTile.exhausted && advantageTile.faction == actionRound.actingPlayer.faction)
        {
            playerAction.player = actionRound.actingPlayer;
            playerAction.Try(() => { });
        }
    }
}
