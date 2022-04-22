using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

public abstract class Action : SerializedMonoBehaviour
{
    public string actionText;
    public List<Conditional> conditionals = new List<Conditional>();
    public List<Command> commands = new List<Command>();

    public void Try(UnityAction callback) { if (Can()) { Game.Log(actionText); Do(callback); }  }
    public virtual bool Can() => conditionals.All(c => c.Test(this)); 
    protected abstract void Do(UnityAction callback); 
}
