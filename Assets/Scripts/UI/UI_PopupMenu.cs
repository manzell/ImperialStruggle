using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening; 

public class UI_PopupMenu : MonoBehaviour, IPointerClickHandler
{
    static GameObject popupMenuContainer; // We only want 1 open at a time, this will ensure we don't always reinstantiate the thing\
    bool open = false;
    GraphicSettings settings; 

    void Awake()
    {
        settings = FindObjectOfType<Game>().graphicSettings; 
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
        /*
        Space space = GetComponent<Space>();
        ActionRound actionRound = Phase.CurrentPhase.GetComponent<ActionRound>();

        if (actionRound != null)
        {
            foreach (PlayerAction action in space.standardActions)
                action.actingPlayer = actionRound.actingFaction;

            List<PlayerAction> actions = space.standardActions.Where(action => action.Can()).ToList();

            if (actions.Count > 0)
            {
                GameObject popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;
                RectTransform rect = popupActionPrefab.GetComponent<RectTransform>();

                if(popupMenuContainer == null)
                    popupMenuContainer = Instantiate(settings.PopupMenu, GetComponentInParent<Canvas>().transform);

                popupMenuContainer.transform.position = transform.position;
                popupMenuContainer.GetComponent<RectTransform>().DOSizeDelta(new Vector2(rect.sizeDelta.x, rect.sizeDelta.y * actions.Count), 0.5f);

                foreach (Transform child in popupMenuContainer.transform)
                    Destroy(child.gameObject);

                foreach (PlayerAction action in actions)
                {
                    UI_MenuAction popupMenuAction = Instantiate(popupActionPrefab, popupMenuContainer.transform).GetComponent<UI_MenuAction>();
                    popupMenuAction.SetAction(action);
                    popupMenuAction.SetMenu(this);
                }
            }
        }
        */
    }

    public void Close()
    {
        open = false;
        
        if(popupMenuContainer != null)
        {
            RectTransform popupRectTransform = popupMenuContainer?.GetComponent<RectTransform>();
            popupRectTransform.DOSizeDelta(new Vector2(popupRectTransform.sizeDelta.x, 0), 0.5f).OnComplete(() => Destroy(popupMenuContainer));
        }
    }
}