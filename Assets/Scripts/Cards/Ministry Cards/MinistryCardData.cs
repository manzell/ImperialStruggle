using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu]
public class MinistryCardData : SerializedScriptableObject
{
    public Faction faction;
    public List<Game.Keyword> keywords = new List<Game.Keyword>();
    public List<Game.Era> eras;
    [TextArea] public string cardText;
}