using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using TMPro; 

public class UI_SelectionWindow<T> : MonoBehaviour where T : ISelectable
{
    
    [SerializeField] GameObject tileArea, buttonArea;
    public Button okButton, resetButton;
    public TextMeshProUGUI windowTitle;
    [SerializeField] GameObject defaultTilePrefab, ministryCardPrefab, eventCardPrefab;

    Dictionary<T, GameObject> itemLookup = new Dictionary<T, GameObject>();

    public void AddTile(T item, SelectionController<T>.Selection selector)
    {
        GameObject tile = Instantiate(GetTilePrefab(item), tileArea.transform);
        ClickHandler toggle = tile.AddComponent<ClickHandler>();

        tile.GetComponent<I_UITitle>().SetTitle(GetTileName(item));         
        toggle.pointerClickEvent.AddListener(() => selector.SelectItem(item));
        itemLookup.Add(item, tile); 
    }

    public void SetTitle(string title) => windowTitle.text = title;

    GameObject GetTilePrefab(T tile)
    {
        if (tile is MinistryCard)
            return ministryCardPrefab;
        else if (tile is EventCard)
            return eventCardPrefab;
        else
            return defaultTilePrefab;
    }

    string GetTileName(T item) => item is MonoBehaviour g ? g.name : item.ToString(); 

    public void Deselect(T item)
    {
        if(itemLookup.TryGetValue(item, out GameObject tile))
            tile.GetComponent<UI_SelectionTile>().RemoveHighlight(); 
    }

    public void Select(T item)
    {
        if (itemLookup.TryGetValue(item, out GameObject tile))
        {
            tile.GetComponent<UI_SelectionTile>().AddHighlight();
        }
    }
}
