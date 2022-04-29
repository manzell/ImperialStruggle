using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class SelectionController : MonoBehaviour
{
    string headerText;
    [SerializeField] GameObject selectionWindowPrefab;
    
    UI_SelectionWindow uiSelectionWindow;
    int maxSelectable;

    public UnityAction callback; 

    public UI_SelectionWindow Summon<T>(List<T> items, int maxSelectable)
    {
        uiSelectionWindow = GameObject.Instantiate(selectionWindowPrefab, this.transform).GetComponent<UI_SelectionWindow>();

        this.maxSelectable = maxSelectable; // is this needed?

        items.ForEach(item => { // is this even necessary? Should uiSelectionWindow have a pseudo-constructor? 
            uiSelectionWindow.AddTile(item);
        });

        return uiSelectionWindow; // is this needed? 
    }

    public void SetTitle(string title) => uiSelectionWindow.SetTitle(title);
    public void Dismiss() => Destroy(uiSelectionWindow.gameObject);
}