using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace ImperialStruggle
{
    public class UI_SelectionWindow : MonoBehaviour
    {
        [SerializeField] GameObject tileArea, buttonArea;
        [SerializeField] UI_SelectionTile defaultTilePrefab; //, ministryCardPrefab, eventCardPrefab;
        [SerializeField] TextMeshProUGUI windowTitle;

        public Button okButton, resetButton, passButton, cancelButton;

        public void Open<T>(Selection<T> selection) where T : ISelectable
        {
            foreach (T item in selection.selectableItems)
            {
                UI_SelectionTile tile = Instantiate(GetTilePrefab(item), tileArea.transform);
                tile.Setup(item);

                ClickHandler toggle = tile.gameObject.AddComponent<ClickHandler>();
                toggle.pointerClickEvent += () => selection.Select(item);
                // TODO: Add Pass and Cancel buttons
            }

            okButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                selection.OnSubmit?.Invoke(selection);
                Destroy(gameObject); 
            });
        }

        public void SetTitle(string title) => windowTitle.text = title;

        public UI_SelectionTile GetTilePrefab<T>(T item)
        {
            //if (item is MinistryCard) return ministryCardPrefab;
            //if (item is EventCard) return eventCardPrefab;
            return defaultTilePrefab;
        }
    }
}