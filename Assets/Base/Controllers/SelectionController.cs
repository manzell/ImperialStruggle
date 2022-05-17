using System; 
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class SelectionController : MonoBehaviour
{
    [SerializeField] GameObject selectionWindowPrefab;

    public Selection Select(List<ISelectable> list, int num)
    {
        Selection selection = new Selection(list, num);

        if (num > 0)
            selection.OpenSelectionWindow(selectionWindowPrefab, transform);

        return selection; 
    }

    public class Selection
    {
        public enum ItemSelectStatus { Unselected, Selected, Rejected }
        public Dictionary<ISelectable, ItemSelectStatus> selectableItems = new Dictionary<ISelectable, ItemSelectStatus>();
        public UnityAction<List<ISelectable>> callback;
        public List<ISelectable> selectedItems = new List<ISelectable>(); 
        int maxSelectable; 

        UI_SelectionWindow uiSelectionWindow;

        public void SetTitle(string title) => uiSelectionWindow.SetTitle(title);

        public Selection(List<ISelectable> items, int maxSelectable)
        {
            this.maxSelectable = maxSelectable;

            selectableItems.Clear();
            foreach (ISelectable item in items)
                selectableItems.Add(item, ItemSelectStatus.Unselected); 
        }

        public void OpenSelectionWindow(GameObject prefab, Transform transform)
        {
            uiSelectionWindow = Instantiate(prefab, transform).GetComponent<UI_SelectionWindow>();
            uiSelectionWindow.okButton.onClick.AddListener(() => callback.Invoke(selectedItems));
            uiSelectionWindow.okButton.onClick.AddListener(CloseSelectionWindow);

            foreach(ISelectable item in selectableItems.Keys)
                uiSelectionWindow.AddTile(item, this);
        }

        void CloseSelectionWindow()
        {
            Destroy(uiSelectionWindow.gameObject); 
        }

        public void SelectItem(ISelectable item)
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
                    ISelectable _item = selectedItems.First();
                    selectableItems[_item] = ItemSelectStatus.Unselected;
                    selectedItems.Remove(_item);
                    uiSelectionWindow.Deselect(_item);
                }
            }
        }

    }
}