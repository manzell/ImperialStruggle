using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFort : GameAction
{
    MilSpace fort;

    public DamageFort(Game.Faction faction, MilSpace fort)
    {
        actingFaction = faction;
        this.fort = fort;
        Do(faction);
    }

    public override void Do(Game.Faction faction)
    {
        Debug.Log($"{faction} adds a Damage Market to {fort}-{fort.flag}");
        fort.damaged = true;
    }
    public override void Undo() => fort.damaged = false;
}