using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

// A GameAction is the brief encapsulation of some change to the GameState, as well as the Undo function
// GameActions are logged on each ActionRound 

public class GameAction
{
    public Game.Faction actingFaction; 

    public virtual bool Can(Game.Faction faction) { return true; }
    public virtual void Do(Game.Faction faction) { }
    public virtual void Undo() { }   
}
