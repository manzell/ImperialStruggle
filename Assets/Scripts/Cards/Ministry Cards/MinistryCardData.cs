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
    public List<MinistryCard.Keyword> keywords = new List<MinistryCard.Keyword>();
    public List<Phase.Era> eras;
    [TextArea] public string cardText;
}