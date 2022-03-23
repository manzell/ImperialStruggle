using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq; 

public class UI_Space : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Coroutine Popup, ClosePopup;
    static GameObject _popupMenu;
    public Space space; 

    void Awake()
    {
        UI_GameBoard.clickEvent.AddListener(ped => { Debug.Log("CLick Listened To"); Close(); });
    }

    public void OnPointerEnter(PointerEventData eventData) => Popup = StartCoroutine(openPopup(0.5f));
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(Popup);

        if(_popupMenu != null && ClosePopup == null)
            ClosePopup = StartCoroutine(closePopup(3f));
    }

    IEnumerator closePopup(float f)
    {
        yield return new WaitForSeconds(f);
        Close(); 
    }

    IEnumerator openPopup(float f)
    {
        yield return new WaitForSeconds(f);
        Open(); 
    }

    void Open()
    {
        Action[] actions = GetComponents<Action>().Where(action => action.Can(UI_PlayerBoard.faction)).ToArray();
        if (actions.Length > 0)
        {
            GameObject popupAction = FindObjectOfType<Game>().graphicSettings.PopupAction;
            GameObject menu = _popupMenu == null ? Instantiate(FindObjectOfType<Game>().graphicSettings.PopupMenu, GetComponentInParent<Canvas>().transform) : _popupMenu;
            RectTransform rect = menu.GetComponent<RectTransform>();

            rect.sizeDelta = new Vector2(rect.sizeDelta.x, popupAction.GetComponent<RectTransform>().sizeDelta.y * actions.Length);
            menu.transform.position = transform.position;
            _popupMenu = menu;

            foreach (Transform child in menu.transform)
                Destroy(child.gameObject);

            for (int i = 0; i < actions.Length; i++)
            {
                GameObject g = Instantiate(popupAction, menu.transform);
                g.GetComponent<UI_MenuAction>().SetAction(actions[i]);
            }
        }
    }

    void Close()
    {
        Debug.Log("Close called!"); 

        if (_popupMenu)
        {
            Destroy(_popupMenu.gameObject);
            _popupMenu = null;
            ClosePopup = null; 
        }
    }
}