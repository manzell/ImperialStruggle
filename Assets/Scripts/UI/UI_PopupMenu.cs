using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class UI_PopupMenu : MonoBehaviour
    {
        static GameObject popupMenuContainer;
        bool open = false;
        GraphicSettings settings;

        void Awake()
        {
            settings = FindObjectOfType<Game>().graphicSettings;
        }

        public void Open(Space space)
        {
            Debug.Log($"Open({space})"); 
            if(!open)
            {
                Game.ActivePlayer.Actions.OfType<TargetSpaceAction>().ForEach(action => action.SetSpace(space));
                Player player = Game.ActivePlayer;
                List<PlayerAction> actions = new();

                foreach (PlayerAction action in player.Actions)
                {
                    if (action is TargetSpaceAction targetSpaceAction)
                    {
                        targetSpaceAction.SetSpace(space);

                        if (action.Can())
                            actions.Add(action);
                    }
                }

                if (actions.Count > 0)
                {
                    GameObject popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;
                    RectTransform rect = popupActionPrefab.GetComponent<RectTransform>();

                    if (popupMenuContainer == null)
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

                    open = true; 
                }
            }
        }

        public void Close()
        {
            open = false;

            if (popupMenuContainer != null)
            {
                RectTransform popupRectTransform = popupMenuContainer?.GetComponent<RectTransform>();
                popupRectTransform.DOSizeDelta(new Vector2(popupRectTransform.sizeDelta.x, 0), 0.5f).OnComplete(() => Destroy(popupMenuContainer));
            }
        }
    }
}