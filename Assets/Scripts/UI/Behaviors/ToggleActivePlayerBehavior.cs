using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ToggleActivePlayerBehavior : MonoBehaviour
{
    public void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Debug.Log("Toggle Game Acting Player and UI"); 
        }
    }
}
