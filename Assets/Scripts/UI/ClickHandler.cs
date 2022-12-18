using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using UnityEngine.EventSystems;
using System.Linq; 

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public System.Action pointerClickEvent;
    public void OnPointerClick(PointerEventData eventData) => pointerClickEvent?.Invoke();
}
