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

    [HideInInspector] public UnityEvent<Action> onActionEvent = new UnityEvent<Action>();

    public void Try(UnityAction callback) 
    {
        if (Can())
        {
            Game.Log(actionText);
            Do(() => Resolve(callback));
            onActionEvent.Invoke(this);
        }
        else Resolve(callback); 
    }

    public virtual bool Can() => conditionals.All(c => c.Test(this)); 
    protected abstract void Do(UnityAction callback); 
    void Resolve(UnityAction callback) => callback.Invoke(); 
}
