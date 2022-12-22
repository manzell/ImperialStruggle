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
        static GameObject popupMenuContainer;
        static bool open;
        Space space; 
        GraphicSettings settings; 

        void Awake()
        {
            settings = FindObjectOfType<Game>().graphicSettings;
            Game.startGameEvent += () => space = GetComponent<UI_Space>().Space; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (open)
                Close();

            Open(space);
        }

        public void Open(Space space)
        {
            Debug.Log("Opening");
            open = true; 
            Player player = Game.ActivePlayer;
            List<PlayerAction> actions = new();

            foreach (PlayerAction action in player.Actions.Where(action => action.Eligible(space)))
            {
                if (action is TargetSpaceAction targetSpaceAction)
                {
                    targetSpaceAction.SetSpace(space);
                    actions.Add(action);
                }
            }

            if (actions.Count > 0)
            {
                GameObject popupActionPrefab = FindObjectOfType<Game>().graphicSettings.PopupAction;
                Vector2 menuSize = popupActionPrefab.GetComponent<RectTransform>().sizeDelta; 

                popupMenuContainer = Instantiate(settings.PopupMenu, FindObjectOfType<UI_GameBoard>().transform);

                popupMenuContainer.transform.position = transform.position; 
                popupMenuContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(menuSize.x, menuSize.y * actions.Count);

                foreach (Transform child in popupMenuContainer.transform)
                    Destroy(child.gameObject);

                foreach (PlayerAction action in actions)
                {
                    UI_MenuAction popupMenuAction = Instantiate(popupActionPrefab, popupMenuContainer.transform).GetComponent<UI_MenuAction>();
                    popupMenuAction.SetAction(action);
                }
            }
        }

        public static void Close()
        {
            Debug.Log("Closing");
            open = false; 
            if (popupMenuContainer != null)
                Destroy(popupMenuContainer);
        }
    }
}