using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class PlayerAction : BaseAction
{
    public Player player;
    UnityAction callback; 

    protected override void Do(UnityAction callback)
    {   
        this.callback = callback;

        if (Can())
            commands.ForEach(command => command.Do(this));
    }

    protected virtual void Finish(List<object> returns)
    {
        callback.Invoke(); 
    }

    public override bool Can() => conditionals.All(c => c.Test(this));
}