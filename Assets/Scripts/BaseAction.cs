using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

public abstract class GameAction : SerializedMonoBehaviour
{
    public string actionName;
    public string actionText;
    public List<Conditional> conditionals = new List<Conditional>();
    public List<Command> commands = new();

    public void Execute()
    {
        if (Can())
        {
            Do();

            commands.ForEach(command => command.Do(this)); 
            Phase.CurrentPhase.executedActions.Push(this);
        }
    }

    public virtual bool Can()
    {
        bool retVal = conditionals.All(c => c.Test(this));
        return retVal;         
    }

    public virtual void Undo()
    {
        commands.ForEach(command => command.Undo()); 
        Debug.Log(this == Phase.CurrentPhase.executedActions.Pop());
    }

    protected abstract void Do(); 
}
