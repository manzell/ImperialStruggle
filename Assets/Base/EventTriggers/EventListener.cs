using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public Trigger trigger;
    public string reply;

    void Awake() => trigger.onTrigger += Reply; 
    void Reply() => Debug.Log(reply);
}
