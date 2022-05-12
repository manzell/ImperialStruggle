using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using UnityEngine.EventSystems;
using System.Linq; 

public class ClickToggleSelect : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent pointerClickEvent = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        pointerClickEvent.Invoke();

        // Move all the below to a function that's registered to onSelect event
        Image image = GetComponents<Image>().Last(); // Instead let's use an interface for Selectable Tile Objects that guarantees a Highlighting Image
        
        if(image)
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a == 1 ? 0f : 1f);
    }
}
