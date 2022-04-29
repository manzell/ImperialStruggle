using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using System.Linq; 

public class ClickToggleSelect : MonoBehaviour, IPointerClickHandler
{
    UI_SelectionWindow selectionWindow;

    public void OnPointerClick(PointerEventData eventData)
    {
        Image image = GetComponents<Image>().Last();

        float alpha = image.color.a == 1 ? 0f : 1f;
    }

    public void SetSelectionWindow(UI_SelectionWindow window) => selectionWindow = window;
}
