using System.Linq; 
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public abstract class UI_Space : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Space Space => space; 
        [SerializeField] protected abstract Space space { get; }
        [SerializeField] protected Image highlight, background, trim;
        [SerializeField] protected TextMeshProUGUI spaceName;
        [SerializeField] UI_PopupMenu popup; 

        private void Awake()
        {
            Game.startGameEvent += Style;
            Game.setActivePlayerEvent += player => Style(); 
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