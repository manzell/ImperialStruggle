using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

// A Command is how we directly change the GameState. It also stashes information on how to Undo itself once it's triggered status is set to true
public class Command : SerializedMonoBehaviour
{
    public Game.Faction targetFaction = Game.Faction.Neutral;
    public string commandDescription; 
    [ShowInInspector] protected bool triggered = false; 

    public virtual void Do(Game.Faction actingFaction) { triggered = true; }
    public virtual void Undo() { }
}
