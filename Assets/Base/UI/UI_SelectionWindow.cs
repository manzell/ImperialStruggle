using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_SelectionWindow : MonoBehaviour
{
    [SerializeField] GameObject tileArea, buttonArea;
    [SerializeField] Button okButton, resetButton;
    [SerializeField] TextMeshProUGUI windowTitle;
    [SerializeField] GameObject defaultTilePrefab, ministryCardPrefab, eventCardPrefab;


    public enum ItemSelectStatus { Unselected, Selected, Rejected }
    public Dictionary<object, ItemSelectStatus> itemSelectStatus = new Dictionary<object, ItemSelectStatus>();

    public List<object> selectedTiles; 

    public void AddTile(object item)
    {
        GameObject tile = Instantiate(GetTilePrefab(item), tileArea.transform);

        tile.name = GetTileName(item);

        itemSelectStatus.Add(tile, ItemSelectStatus.Unselected);
        
        ClickToggleSelect toggle = tile.AddComponent<ClickToggleSelect>();
        toggle.SetSelectionWindow(this); 
        //the ClickToggleSelect Component, every time a click is 
    }

    public void SetTitle(string title) => windowTitle.text = title;

    GameObject GetTilePrefab<T>(T item)
    {
        if (item is MinistryCard)
            return ministryCardPrefab;
        else if (item is EventCard)
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
