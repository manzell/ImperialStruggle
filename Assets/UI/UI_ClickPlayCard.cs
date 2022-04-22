using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UI_Card))]
public class UI_ClickPlayCard : MonoBehaviour, IPointerClickHandler
{
    public Game.Faction faction; 
    public void OnPointerClick(PointerEventData eventData)
    {
        //Phase.currentPhase.gameActions.Add(new PlayCard((Phase.currentPhase as ActionRound).actingFaction, GetComponent<UI_Card>().card as EventCard));
    }
}
