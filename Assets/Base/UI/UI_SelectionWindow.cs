using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; 
using TMPro; 

public class UI_SelectionWindow : MonoBehaviour
{
    [SerializeField] GameObject tileArea, buttonArea;
    public Button okButton, resetButton;
    public TextMeshProUGUI windowTitle;
    [SerializeField] GameObject defaultTilePrefab, ministryCardPrefab, eventCardPrefab;
    

    public void AddTile<T>(T item, SelectionController.Selection<T> selector)
    {
        GameObject tile = Instantiate(GetTilePrefab(item), tileArea.transform);
        tile.GetComponent<I_UITitle>().SetTitle(GetTileName(item)); 
        tile.name = GetTileName(item);
        
        ClickToggleSelect toggle = tile.AddComponent<ClickToggleSelect>();
        toggle.pointerClickEvent.AddListener(() => selector.Select(item)); 
    }

    public void SetTitle(string title) => windowTitle.text = title;

    GameObject GetTilePrefab<T>(T tile)
    {
        if (tile is MinistryCard)
            return ministryCardPrefab;
        else if (tile is EventCard)
            return eventCardPrefab;
        else
            return defaultTilePrefab;
    }

    string GetTileName<T>(T item)
    {
        if (item is MinistryCard)
            return (item as MinistryCard).name;
        else if (item is EventCard)
            return (item as EventCard).name; 
        else
            return item.ToString();
    }
}
