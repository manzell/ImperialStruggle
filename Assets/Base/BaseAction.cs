using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

public abstract class BaseAction : SerializedMonoBehaviour
{
    public string actionName;
    public string actionText;
    public List<Conditional> conditionals = new List<Conditional>();
    public List<Command> commands = new List<Command>();

    public bool Try(UnityAction callback) 
    {
        if (Can())
        {
            Do(callback);
            return true; 
        }
        else
        {
            callback.Invoke();
            return false; 
        }
    }

    public virtual bool Can()
    {
        bool retVal = conditionals.All(c => c.Test(this));
        return retVal;         
    }

    protected abstract void Do(UnityAction callback); 
}
