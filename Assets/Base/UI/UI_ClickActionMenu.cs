using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq; 

public class UI_ClickActionMenu : MonoBehaviour, IPointerDownHandler
{
    static GameObject _popupMenu; // We only want 1 open at a time, this will ensure we don't always reinstantiate the thing

    bool open = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!open) Open(); 
    }

    void Open()
    {
        Space space = GetComponent<Space>();
        GameObject popupMenuContainer = _popupMenu == null ? Instantiate(FindObjectOfType<Game>().graphicSettings.PopupMenu, GetComponentInParent<Canvas>().transform) : _popupMenu;
        GameObject popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;

        RectTransform rect = popupMenuContainer.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(rect.sizeDelta.x, popupActionPrefab.GetComponent<RectTransform>().sizeDelta.y * space.standardActions.Count);
        popupMenuContainer.transform.position = transform.position;

        foreach (Transform child in popupMenuContainer.transform)
            Destroy(child.gameObject);

        foreach (PlayerAction action in space.standardActions)
        {
            GameObject popupMenuItem = Instantiate(popupActionPrefab, popupMenuContainer.transform);
            popupMenuItem.GetComponent<UI_MenuAction>().SetAction(action);
        }

        _popupMenu = popupMenuContainer;
    }

    void Close()
    {
    }
}