using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening; 

public class UI_PopupMenu : MonoBehaviour, IPointerClickHandler
{
    static GameObject popupMenuContainer; // We only want 1 open at a time, this will ensure we don't always reinstantiate the thing

    bool open = false;

    void Awake()
    {
        UI_GameBoard.clickEvent.AddListener(ped => Close()); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!open)
        {
            Open();
            open = true; 
        }
    }

    void Open()
    {
        Space space = GetComponent<Space>();
        ActionRound actionRound = Phase.currentPhase.GetComponent<ActionRound>();

        if (actionRound != null)
        {
            foreach (PlayerAction action in space.standardActions)
                action.player = actionRound.actingPlayer;

            List<PlayerAction> actions = space.standardActions.Where(action => action.Can()).ToList();

            if (actions.Count > 0)
            {
                GameObject popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;
                popupMenuContainer = popupMenuContainer == null ? Instantiate(FindObjectOfType<Game>().graphicSettings.PopupMenu, GetComponentInParent<Canvas>().transform) : popupMenuContainer;

                RectTransform rect = popupActionPrefab.GetComponent<RectTransform>();

                popupMenuContainer.transform.position = transform.position;
                popupMenuContainer.GetComponent<RectTransform>().DOSizeDelta(new Vector2(rect.sizeDelta.x, rect.sizeDelta.y * actions.Count), 0.5f);

                foreach (Transform child in popupMenuContainer.transform)
                    Destroy(child.gameObject);

                foreach (PlayerAction action in actions)
                {
                    UI_MenuAction popupMenu = Instantiate(popupActionPrefab, popupMenuContainer.transform).GetComponent<UI_MenuAction>();
                    popupMenu.SetAction(action);
                    popupMenu.SetMenu(this);
                }
            }
        }
    }

    public void Close()
    {
        open = false; 
        RectTransform popup = popupMenuContainer.GetComponent<RectTransform>(); 
        popup.DOSizeDelta(new Vector2(popup.sizeDelta.x, 0), 0.5f).OnComplete(() => Destroy(popupMenuContainer)); 
    }
}