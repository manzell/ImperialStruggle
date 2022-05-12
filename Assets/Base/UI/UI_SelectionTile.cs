using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SelectionTile : MonoBehaviour, I_UITitle
{
    public TextMeshProUGUI title;

    public void SetTitle(string s) => title.text = s; 
}
