using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq;
using Sirenix.Utilities;

[System.Serializable]
public abstract class GameAction : SerializedMonoBehaviour
{
    public string actionName;
    public string actionText;
    public List<Conditional> conditionals = new();
    protected Stack<Command> commands = new();

    protected abstract void Do();

    public virtual bool Can()
    {
        bool retVal = conditionals.All(c => c.Test(this));
        return retVal;
    }

    public void Execute()
    {
        if (Can())
        {
            Do();
            commands.ForEach(command => command.Do(this)); 
            Phase.CurrentPhase.executedActions.Push(this);
        }
    }

    public void Undo()
    {
        while (commands.TryPop(out Command command))
            command.Undo(); 
    }
}