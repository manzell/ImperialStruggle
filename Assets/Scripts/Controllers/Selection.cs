using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace ImperialStruggle
{
    public class Selection<T> : IEnumerable<T> where T : ISelectable
    {
        public IEnumerable<T> selectableItems { get; private set; }
        public List<T> selectedItems { get; private set; }
        public int maxSelectable { get; private set; } = 1;

        public System.Action<Selection<T>> OnSelect, OnCancel, OnPass;

        public Selection(Player player, IEnumerable<T> items, System.Action<Selection<T>> callback)
        {
            selectableItems = items.ToList();
            player.UI.Select(this);
            OnSelect = callback;
        }

        public Selection(Player player, IEnumerable<T> items, System.Action<Selection<T>> callback, int numItems) : this(player, items, callback)
        {
            this.maxSelectable = numItems;
        }

        public void Select(T item)
        {
            if (selectedItems.Contains(item))
                selectedItems.Remove(item);
            else if (selectedItems.Count() < maxSelectable)
                selectedItems.Add(item);
            else
            {
                selectedItems.Remove(selectedItems.First());
                selectedItems.Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => selectableItems.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}