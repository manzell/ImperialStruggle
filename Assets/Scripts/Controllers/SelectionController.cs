using System; 
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class SelectionController<T> : MonoBehaviour where T : ISelectable
{
    [SerializeField] GameObject selectionWindowPrefab;

    public Selection Select(IEnumerable<T> list, int num)
    {
        Selection selection = new Selection(list);

        if (num > 0)
            selection.OpenSelectionWindow(selectionWindowPrefab, transform);

        return selection; 
    }

    public class Selection
    {
        public enum ItemSelectStatus { Unselected, Selected, Rejected }
        public Dictionary<T, ItemSelectStatus> selectableItems = new Dictionary<T, ItemSelectStatus>();
        public System.Action<IEnumerable<T>> multicallback;
        public System.Action<T> solocallback;
        public List<T> selectedItems = new List<T>();
        int maxSelectable = 1;  
        UI_SelectionWindow<T> uiSelectionWindow;

        public void SetTitle(string title) => uiSelectionWindow.SetTitle(title);

        public Selection(IEnumerable<T> items)
        {
            selectableItems.Clear();
            foreach (T item in items)
                selectableItems.Add(item, ItemSelectStatus.Unselected);
        }

        public Selection(IEnumerable<T> items, System.Action<T> callback) : this(items)
        {
            solocallback = callback;
        }

        public Selection(IEnumerable<T> items, System.Action<IEnumerable<T>> callback, int numItems): this(items)
        {
            multicallback = callback;
            this.maxSelectable = numItems; 
        }

       
        public void OpenSelectionWindow(GameObject prefab, Transform transform)
        {
            uiSelectionWindow = Instantiate(prefab, transform).GetComponent<UI_SelectionWindow<T>>();

            //uiSelectionWindow.okButton.onClick.AddListener(maxSelectable == 1 ? solocallback : multicallback);
            uiSelectionWindow.okButton.onClick.AddListener(CloseSelectionWindow);

            foreach(T item in selectableItems.Keys)
                uiSelectionWindow.AddTile(item, this);
        }

        void CloseSelectionWindow()
        {
            Destroy(uiSelectionWindow.gameObject); 
        }

        public void SelectItem(T item)
        {
            // This gets called on a click, essentially. 

            if (selectedItems.Contains(item)) // This means we're deselecting
            {
                selectedItems.Remove(item);
                selectableItems[item] = ItemSelectStatus.Unselected;
                uiSelectionWindow.Deselect(item); 
            }
            else
            {
                selectableItems[item] = ItemSelectStatus.Selected;
                selectedItems.Add(item);
                uiSelectionWindow.Select(item); 

                while(maxSelectable != 0 && selectedItems.Count > maxSelectable)
                {
                    T _item = selectedItems.First();
                    selectableItems[_item] = ItemSelectStatus.Unselected;
                    selectedItems.Remove(_item);
                    uiSelectionWindow.Deselect(_item);
                }
            }
        }
    }
}