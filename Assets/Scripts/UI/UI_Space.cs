using System.Linq; 
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public abstract class UI_Space : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected abstract Space Space { get; }
        [SerializeField] protected Image highlight, background, trim;
        [SerializeField] protected TextMeshProUGUI spaceName;

        private void Awake()
        {
            Game.startGameEvent += Style;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"On Pointer Click ({Space})"); 
            UI_PopupMenu popup = gameObject.AddComponent<UI_PopupMenu>();
            popup.Open(Space); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            highlight.enabled = true; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            highlight.enabled = false; 
        }

        public abstract void Style();
    }
}