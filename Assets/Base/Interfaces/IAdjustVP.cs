using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustVP
{
    public Game.Faction faction { get; }
    public int vp { get; }
}
