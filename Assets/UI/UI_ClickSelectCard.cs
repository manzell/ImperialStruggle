using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ClickSelectCard : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) =>
        GetComponentInParent<UI_CardSelector>().Select(GetComponent<UI_Card>().card); 
}