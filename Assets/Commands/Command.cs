using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

// A Command is how we directly change the GameState. It also stashes information on how to Undo itself once it's triggered status is set to true
public class Command : Object
{
    public Game.Faction actingFaction;
    protected bool triggered = false; 
    public virtual bool Can(Game.Faction faction) { return true; }
    public virtual void Do(Game.Faction faction) { triggered = true; }
    public virtual void Undo() { }   
}
