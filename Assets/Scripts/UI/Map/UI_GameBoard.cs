using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using System.Linq;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class UI_GameBoard : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
    {
        [SerializeField] GameObject mapContainer, lineContainer, linePrefab;
        [SerializeField] float speed = 0.1f;
        Vector2 previousPosition;

        [SerializeField] public Dictionary<(Space, Space), LineRenderer> connections = new Dictionary<(Space, Space), LineRenderer>();

        public void OnBeginDrag(PointerEventData eventData) => previousPosition = Mouse.current.position.ReadValue();

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 frameSize = mapContainer.GetComponent<RectTransform>().sizeDelta;
            Vector2 boardSize = GetComponent<RectTransform>().sizeDelta;
            Vector3 drag = (Mouse.current.position.ReadValue() - previousPosition) * speed;

            Vector3 newPosition = transform.position + drag;

            bool maxXrespected = (newPosition.x + boardSize.x / 2) > (mapContainer.transform.position.x + frameSize.x / 2);
            bool maxYrespected = (newPosition.y + boardSize.y / 2) > (mapContainer.transform.position.y + frameSize.y / 2);
            bool minXrespected = (newPosition.x - boardSize.x / 2) < (mapContainer.transform.position.x - frameSize.x / 2);
            bool minYrespected = (newPosition.y - boardSize.y / 2) < (mapContainer.transform.position.y - frameSize.y / 2);

            if (maxXrespected && maxYrespected && minXrespected && minYrespected)
            {
                transform.position += drag;
                previousPosition = Mouse.current.position.ReadValue();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UI_PopupMenu.Close(); 
        }
    }
}