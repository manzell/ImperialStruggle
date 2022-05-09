using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class SelectionController : MonoBehaviour
{
    [SerializeField] GameObject selectionWindowPrefab;

    public Selection<T> Select<T>(List<T> list, int num) => new Selection<T>(list, num, selectionWindowPrefab, transform);

    public class Selection<T>
    {
        Dictionary<T, bool> selectableItems = new Dictionary<T, bool>(); // Objected, Selected
        public UnityAction<List<T>> callback;
        int maxSelectable; 

        UI_SelectionWindow uiSelectionWindow;

        public Selection(List<T> items, int maxSelectable, GameObject prefab, Transform transform)
        {
            selectableItems.Clear();
            uiSelectionWindow = Instantiate(prefab, transform).GetComponent<UI_SelectionWindow>();
            this.maxSelectable = maxSelectable;

            items.ForEach(item => { // is this even necessary? Should uiSelectionWindow have a pseudo-constructor? 
                selectableItems.Add(item, false);
                uiSelectionWindow.AddTile(item);
            });

        }

        public void Select(T item)
        {
            if (selectableItems.ContainsKey(item) && selectableItems.Where(item => item.Value == true).Count() < maxSelectable) 
                selectableItems[item] = !selectableItems[item];
        }

        public void SetTitle(string title) => uiSelectionWindow.SetTitle(title);

        public List<T> selectedItems => selectableItems.Where(item => item.Value == true).Select(item => item.Key).ToList();

    }
}