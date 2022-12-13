using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : ScriptableObject, ISelectable
{
    public string Name => name; 
    public Sprite resourceIcon;
    public Color resourceColor; 
}
