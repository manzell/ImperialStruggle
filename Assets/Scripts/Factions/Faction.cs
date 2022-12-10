using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI; 

public class Faction : ScriptableObject, ISelectable
{
    public Sprite Icon;
    public Color Color;
    public string Name => name; 
    //public static Faction Britain, France, Spain, USA, Neutral; 
}
