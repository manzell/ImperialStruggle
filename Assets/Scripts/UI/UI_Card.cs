using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_Card : MonoBehaviour
{
    public TMPro.TextMeshProUGUI cardName;
    public ICard card;
    public Image highlight; 

    public void SetCard(ICard card)
    {
        this.card = card;
        cardName.text = card.gameObject.name;
    }

    public void SetHighlight(Color color)
    {
        highlight.gameObject.SetActive(true); 
        highlight.color = new Color(color.r, color.g, color.b, .5f); 
    }

    public void RemoveHighlight()
    {
        highlight.gameObject.SetActive(false);
    }
}
