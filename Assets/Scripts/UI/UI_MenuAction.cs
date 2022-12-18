using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

namespace ImperialStruggle
{
    public class UI_MenuAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Image costIcon, background, highlight;
        [SerializeField] TextMeshProUGUI cost, actionName;

        PlayerAction action;
        UI_PopupMenu popupMenu;

        public void OnPointerEnter(PointerEventData eventData) => highlight.gameObject.SetActive(true);
        public void OnPointerExit(PointerEventData eventData) => highlight.gameObject.SetActive(false);
        public async void OnPointerClick(PointerEventData eventData)
        {
            popupMenu.Close();
            await action.Execute();
        }

        public void SetAction(PlayerAction action)
        {
            this.action = action;
            actionName.text = action.ToString();

            //if (action.actionPointCost.Count > 0)
            //    SetActionCost(action.actionPointCost); 
        }

        public void SetMenu(UI_PopupMenu popupMenu) => this.popupMenu = popupMenu;

        public void SetActionCost(ActionPoints actionPointCost)
        {
            List<ActionPoint> majorActionPoints = actionPointCost.Where(actionPoint => actionPoint.actionTier == ActionPoint.ActionTier.Major).ToList();
            List<ActionPoint> minorActionPoints = actionPointCost.Where(actionPoint => actionPoint.actionTier == ActionPoint.ActionTier.Minor).ToList();

            if (majorActionPoints.Count > 0)
            {
                cost.text = majorActionPoints[0].Value(action).ToString();
                cost.color = Color.black;
                costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[majorActionPoints[0].actionType];
                costIcon.color = Color.white;
            }
            else if (minorActionPoints.Count > 0)
            {
                cost.text = minorActionPoints[0].Value(action).ToString();
                cost.color = Color.gray;
                costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[minorActionPoints[0].actionType];
                costIcon.color = Color.gray;
            }
        }
    }
}