using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.EventSystems;

public class UI_GameBoard : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
{
    public static UnityEvent<PointerEventData> clickEvent = new UnityEvent<PointerEventData>();
    [SerializeField] GameObject container;
    [SerializeField] float speed = 0.1f; 
    Vector3 previousPosition;

    public void OnPointerClick(PointerEventData eventData) => clickEvent.Invoke(eventData);

    public void OnBeginDrag(PointerEventData eventData) => previousPosition = Input.mousePosition;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 frameSize = container.GetComponent<RectTransform>().sizeDelta;
        Vector2 boardSize = GetComponent<RectTransform>().sizeDelta;
        Vector3 drag = (Input.mousePosition - previousPosition) * speed;

        transform.position += drag;

        previousPosition = Input.mousePosition; 
    }
}
