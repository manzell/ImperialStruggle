using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class UI_MenuAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image costIcon, background, highlight;
    [SerializeField] TextMeshProUGUI cost, actionName; 

    public void SetAction(Action action)
    {
        actionName.text = action.actionName;
        cost.text = action.actionCost.ToString();
        costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[action.requiredActionType];
    }

    public void OnPointerEnter(PointerEventData eventData) => highlight.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => highlight.gameObject.SetActive(false);
}
