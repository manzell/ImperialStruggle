using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using DG.Tweening; 

public class UI_SelectionTile : MonoBehaviour, I_UITitle
{
    public TextMeshProUGUI title;
    public Image tileImage, tileHighlightImage; 

    public void SetTitle(string s) => title.text = s;

    public void AddHighlight()
    {
        tileHighlightImage.DOFade(.4f, 0.25f); 
    }

    public void RemoveHighlight()
    {
        tileHighlightImage.DOFade(0f, 0.25f);
    }
}
