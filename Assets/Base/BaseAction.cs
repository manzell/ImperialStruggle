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
    public Trigger trigger;

    public bool Try(UnityAction callback) 
    {
        if (Can())
        {
            Phase.currentPhase.executedActions.Add(this); 
            Do(callback);
            trigger?.onTrigger.Invoke(); 

            return true; 
        }
        else
        {
            callback?.Invoke();
            return false; 
        }
    }

    public virtual bool Can()
    {
        bool retVal = conditionals.All(c => c.Test(this));
        return retVal;         
    }

    public abstract void Do(UnityAction callback); 
}
