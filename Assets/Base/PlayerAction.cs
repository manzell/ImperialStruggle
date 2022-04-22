using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class PlayerAction : Action
{
    public Game.Faction actingFaction;
    public Player player; 

    protected override void Do(UnityAction callback)
    {
        actingFaction = player.faction;

        if (conditionals.All(condition => condition.Test(player)))
            foreach(Command command in commands)
                command.Do(this);

        callback.Invoke(); 
    }
}