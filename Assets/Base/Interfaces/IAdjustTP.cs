using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustTP
{
    public Game.Faction faction { get; }
    int tp { get; }
}
