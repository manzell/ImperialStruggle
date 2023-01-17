using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    [System.Serializable]
    public class Selection<T> : IEnumerable<T> where T : ISelectable
    {
        public IEnumerator<T> GetEnumerator() => selectedItems.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Player player { get; private set; }
        public IEnumerable<T> selectableItems { get; private set; }
        public List<T> selectedItems { get; private set; }
        public int minSelectable { get; private set; } = 1;
        public int maxSelectable { get; private set; } = 1;
        public System.Action<Selection<T>> OnSubmit, OnCancel, OnPass;
        public Task<IEnumerable<T>> Completion => task.Task; 

        UI_SelectionWindow window;
        TaskCompletionSource<IEnumerable<T>> task;

        public Selection(Player player, IEnumerable<T> items)
        {
            if (items.Count() > 0)
            {
                this.player = player;
                selectableItems = items;
                selectedItems = new();
                task = new();
                window = player.UI.Select(this);
            }
            else
                task.SetResult(null);
        }

        public Selection(Player player, IEnumerable<T> items, System.Action<Selection<T>> callback) : this(player, items)
        {
            OnSubmit += callback;
            OnSubmit += SetResults;
        }

        public void Add(T item)
        {
            selectableItems.Append(item);
            window.Add(this, item); 
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

        public void SetTitle(string x) => window.SetTitle(x); 

        public void SetMin(int x) => minSelectable = x;
        public void SetMax(int x) => maxSelectable = x; 
        public void SetMinMax(int min, int max) { minSelectable = min; maxSelectable = max; }
    }
}