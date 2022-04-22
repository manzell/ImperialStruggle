using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFort : Command
{
    MilSpace fort;
    Game.Faction actingFaction; 

    public override void Do(Action action)
    {
        Debug.Log($"{actingFaction} adds a Damage Marker to {fort}-{fort.flag}");
        fort.damaged = true;
    }
    public override void Undo() => fort.damaged = false;
}