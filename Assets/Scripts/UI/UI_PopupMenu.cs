using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;

namespace ImperialStruggle
{
    public class UI_PopupMenu : MonoBehaviour, IPointerClickHandler
    {
        public static GameObject popupMenuContainer;
        public static GameObject popupActionPrefab;
        static PointerEventData lastClick;

        void Awake()
        {
            if(popupActionPrefab == null)
                popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;

            if(popupMenuContainer == null)
            {
                popupMenuContainer = Instantiate(FindObjectOfType<Game>().graphicSettings.PopupMenu, FindObjectOfType<UI_GameBoard>().uiOverlay);
                popupMenuContainer.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            lastClick = eventData;
            if (popupMenuContainer.activeSelf)
                Close();
            else if(TryGetComponent(out IPopupMenu popup))
                popup.OpenPopupMenu(); 
        }

        public static void Open(IEnumerable<PlayerAction> actions)
        {
            Debug.Log(popupActionPrefab);
            Debug.Log(popupActionPrefab.GetComponent<RectTransform>());

            Vector2 menuSize = popupActionPrefab.GetComponent<RectTransform>().sizeDelta;
            
            popupMenuContainer.SetActive(true);
            popupMenuContainer.transform.position = lastClick.position; 
            popupMenuContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(menuSize.x, menuSize.y * actions.Count());

            foreach (PlayerAction action in actions)
                Instantiate(popupActionPrefab, popupMenuContainer.transform).GetComponent<UI_MenuAction>().SetAction(action);
        }

        public static void Close()
        {
            foreach (Transform child in popupMenuContainer.transform)
                Destroy(child.gameObject);

            popupMenuContainer.SetActive(false);
        }
    }
}