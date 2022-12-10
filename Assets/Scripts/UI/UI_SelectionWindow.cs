using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ImperialStruggle
{
    public class UI_SelectionWindow : MonoBehaviour
    {
        [SerializeField] GameObject tileArea, buttonArea;
        [SerializeField] GameObject defaultTilePrefab, ministryCardPrefab, eventCardPrefab;
        [SerializeField] TextMeshProUGUI windowTitle;

        public Button okButton, resetButton, passButton, cancelButton;

        public void Open<T>(Selection<T> selection) where T : ISelectable
        {
            tileArea.SetActive(true);

            foreach (T item in selection.selectableItems)
            {
                GameObject tile = Instantiate(GetTilePrefab(item), tileArea.transform);
                tile.name = item.Name;

                ClickHandler toggle = tile.AddComponent<ClickHandler>();
                toggle.pointerClickEvent.AddListener(() => selection.Select(item));

                okButton.onClick.RemoveAllListeners();
                okButton.onClick.AddListener(() =>
                {
                    tileArea.SetActive(false);
                    selection.OnSelect?.Invoke(selection);
                });
                // TODO: Add Pass and Cancel buttons
            }
        }

        public GameObject GetTilePrefab<T>(T item)
        {
            if (item is MinistryCard) return ministryCardPrefab;
            if (item is EventCard) return eventCardPrefab;
            return defaultTilePrefab;
        }
    }
}