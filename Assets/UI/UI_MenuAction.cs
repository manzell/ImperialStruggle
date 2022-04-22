using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class UI_MenuAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image costIcon, background, highlight;
    [SerializeField] TextMeshProUGUI cost, actionName;
    ActionOld action; 

    public void SetAction(ActionOld action)
    {
        this.action = action;
        actionName.text = action.actionName;
        cost.text = action.actionCost.ToString();
        costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[action.requiredActionType];
    }

    public void OnPointerEnter(PointerEventData eventData) => highlight.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => highlight.gameObject.SetActive(false);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (action.Can(UI_PlayerBoard.faction))
            action.Do(UI_PlayerBoard.faction); 
    }
}
