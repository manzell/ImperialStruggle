using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events; 

public class GameAction : Action
{
    public override bool Can() => conditionals.All(condition => condition.Test(this));
    protected override void Do(UnityAction callback)
    {
        if (conditionals.All(condition => condition.Test(this)))
            commands.ForEach(command => command.Do(this));

        callback.Invoke(); 
    }
}