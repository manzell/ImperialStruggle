using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class Selection<T> : IEnumerable<T> where T : ISelectable
    {
        public Player player { get; private set; }
        public IEnumerable<T> selectableItems { get; private set; }
        public List<T> selectedItems { get; private set; }
        public int maxSelectable { get; private set; } = 1;

        public System.Action<Selection<T>> OnSubmit, OnCancel, OnPass;
        public Task<IEnumerable<T>> Completion => task.Task; 
        TaskCompletionSource<IEnumerable<T>> task;

        public IEnumerator<T> GetEnumerator() => selectedItems.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public System.Action<string> SetTitle { get; private set; }

        public Selection(Player player, IEnumerable<T> items)
        {
            this.player = player; 
            selectableItems = items;
            selectedItems = new();
            task = new();
        }

        public Selection(Player player, IEnumerable<T> items, System.Action<Selection<T>> callback) : this(player, items)
        {
            OnSubmit += callback;
            OnSubmit += SetResults;

            UI_SelectionWindow window = player.UI.Select(this);
            SetTitle = window.SetTitle; 
        }

        public Selection(Player player, IEnumerable<T> items, System.Action<Selection<T>> callback, int numItems) : this(player, items, callback)
        {
            this.maxSelectable = numItems;
        }

        public void Select(T item)
        {
            if (!selectedItems.Contains(item))
            {
                item.UISelectionEvent?.Invoke();
                selectedItems.Add(item);
            }
            else
            {
                item.UIDeselectEvent?.Invoke();
                selectedItems.Remove(item);
            }

            if (selectedItems.Count() > maxSelectable)
            {
                item = selectedItems.First();
                item.UIDeselectEvent?.Invoke();
                selectedItems.Remove(item);
            }
        }

        public void SetResults(IEnumerable<T> results) => task.SetResult(results); 

        public Selection(Player player, WarTile warTileToReplace, IEnumerable<WarTile> warTiles, System.Action<Theater, WarTile, WarTile> callback)
        {
            Debug.Log("Cool, a custom Constructor");
        }
    }
}