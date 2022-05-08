using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector; 

// A Command is how we directly change the GameState and it's undo. // ALL this knows is what value to adjust and how much to adjust it by. It doesn't know anything else about 
// The game state. It only knows what comes in via the 

[System.Serializable]
public abstract class Command
{
    public abstract void Do(BaseAction action);
    public virtual void Undo() { }
}
