using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu]
public class Trigger : ScriptableObject
{
    public UnityAction onTrigger;
    public void Do() => onTrigger?.Invoke(); 
}
