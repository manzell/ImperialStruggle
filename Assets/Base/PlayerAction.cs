using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class PlayerAction : Action
{
    public Player player;

    protected override void Do(UnityAction callback)
    {
        if (conditionals.All(condition => condition.Test(player)))
            commands.ForEach(command => command.Do(this)); 

        callback.Invoke(); 
    }
}