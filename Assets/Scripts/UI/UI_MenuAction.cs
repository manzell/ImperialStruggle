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

        public void OnPointerEnter(PointerEventData eventData) => highlight.gameObject.SetActive(action.Can());
        public void OnPointerExit(PointerEventData eventData) => highlight.gameObject.SetActive(false);
        
        public async void OnPointerClick(PointerEventData eventData)
        {
            UI_PopupMenu.Close();
            await action.Execute();
        }

        public void SetAction(PlayerAction action)
        {
            this.action = action;
            actionName.text = action.Name;

            actionName.color = action.Can() ? Color.black : Color.gray; 

            if(action is PurchaseAction purchaseAction)
                SetActionCost(purchaseAction); 
        }

        public void SetActionCost(PurchaseAction purchaseAction)
        {
            if (purchaseAction.ActionCost.tier == ActionPoint.ActionTier.Major)
            {
                cost.color = Color.black;
                costIcon.color = Color.white;
            }
            else if (purchaseAction.ActionCost.tier == ActionPoint.ActionTier.Minor)
            {
                cost.color = Color.gray;
                costIcon.color = Color.gray;
            }

            cost.text = purchaseAction.ActionCost.Value(purchaseAction).ToString();
            costIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[purchaseAction.ActionCost.type];
        }
    }
}