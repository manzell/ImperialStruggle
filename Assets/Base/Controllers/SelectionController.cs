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
        public enum ItemSelectStatus { Unselected, Selected, Rejected }
        Dictionary<T, ItemSelectStatus> selectableItems = new Dictionary<T, ItemSelectStatus>(); // Object object, bool Selected
        public UnityAction<List<T>> callback;
        int maxSelectable; 

        UI_SelectionWindow uiSelectionWindow;

        public Selection(List<T> items, int maxSelectable, GameObject prefab, Transform transform)
        {
            selectableItems.Clear();
            uiSelectionWindow = Instantiate(prefab, transform).GetComponent<UI_SelectionWindow>();
            uiSelectionWindow.okButton.onClick.AddListener(() => callback.Invoke(selectedItems));
            uiSelectionWindow.okButton.onClick.AddListener(Close); 
            this.maxSelectable = maxSelectable;

            items.ForEach(item => { // is this even necessary? Should uiSelectionWindow have a pseudo-constructor? 
                selectableItems.Add(item, ItemSelectStatus.Unselected);
                uiSelectionWindow.AddTile(item, this);
            });
        }

        void Close()
        {
            Destroy(uiSelectionWindow.gameObject); 
        }

        public void Select(T item)
        {
            if (selectableItems.ContainsKey(item) && selectableItems.Where(item => item.Value == ItemSelectStatus.Selected).Count() < maxSelectable)
                selectableItems[item] = ItemSelectStatus.Selected; 
        }

        public void SetTitle(string title) => uiSelectionWindow.SetTitle(title);

        public List<T> selectedItems => selectableItems.Where(item => item.Value == ItemSelectStatus.Selected).Select(item => item.Key).ToList();
    }
}