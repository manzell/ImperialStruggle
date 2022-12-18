using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_SelectionTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image tileImage, tileHighlightImage;
    Color color;
    bool highlighted;
    ISelectable item; 

    public void Setup(ISelectable selectable)
    {
        item = selectable; 
        selectable.UISelectionEvent += AddHighlight;
        selectable.UIDeselectEvent += RemoveHighlight;
        name = selectable.Name;
        SetTitle(selectable.Name);
    }

    public void SetTitle(string s) => title.text = s;

    public void AddHighlight()
    {
        tileHighlightImage.DOFade(.4f, 0.4f);
        highlighted = true;
    }

    public void RemoveHighlight()
    {
        tileHighlightImage.DOFade(0f, 0.15f);
        highlighted = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        color = tileImage.color;

        if(!highlighted)
            tileImage.DOColor(Color.red, 0.2f); 
    }

    public void OnPointerExit(PointerEventData eventData) => tileImage.DOColor(color, .2f); 
}
