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

    PlayerAction action;

    public void SetAction(PlayerAction action)
    {
        Debug.Log(action);
        Debug.Log(action.actionPointCost);
        Debug.Log(action.actionPointCost.Count); 

        actionName.text = action.actionName;

        if (action.actionPointCost.Count > 0)
        {
            List<ActionPoint> majorActionPoints = action.actionPointCost.Where(actionPoint => actionPoint.actionTier == Game.ActionTier.Major).ToList();

            this.action = action; 
            
            if (majorActionPoints.Count > 0)
            {
                cost.text = majorActionPoints[0].Value(action).ToString();
                costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[majorActionPoints[0].actionType];
            }
            else
            {
                cost.text = string.Empty;
                costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[Game.ActionType.None];
            }
        }        
    }

    public void OnPointerEnter(PointerEventData eventData) => highlight.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => highlight.gameObject.SetActive(false);
    public void OnPointerClick(PointerEventData eventData)
    {
        action.Try(() => { }); // Do nothing after executing the Action. 
        // Oh we can close the menu, too? 
    }
}
