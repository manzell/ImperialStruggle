using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEditor;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class UI_SelectionWindow : MonoBehaviour
    {
        [SerializeField] GameObject tileArea, buttonArea;
        [SerializeField] UI_SelectionTile defaultTilePrefab; //, ministryCardPrefab, eventCardPrefab;
        [SerializeField] TextMeshProUGUI windowTitle;

        public Button okButton, resetButton, passButton, cancelButton;

        public void Add<T>(Selection<T> selection, T item) where T : ISelectable
        {
            UI_SelectionTile tile = Instantiate(GetTilePrefab(item), tileArea.transform);
            tile.Setup(item);

            ClickHandler toggle = tile.gameObject.AddComponent<ClickHandler>();
            toggle.pointerClickEvent += () => selection.Select(item);
        }

        public void Open<T>(Selection<T> selection) where T : ISelectable
        {
            selection.selectableItems.ForEach(item => Add(selection, item)); 

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