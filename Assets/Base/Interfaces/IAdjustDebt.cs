using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustDebt
{
    public Game.Faction faction { get; }
    int debt { get; }
}
