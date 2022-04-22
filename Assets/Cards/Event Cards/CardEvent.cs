using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

public abstract class CardEvent : SerializedMonoBehaviour
{
    public enum ConditionalType { Any, All }
    public Game.Faction faction;
    public List<Conditional> conditionals;
    public ConditionalType conditionalType;

    public string text; 
    public abstract void Event();

}