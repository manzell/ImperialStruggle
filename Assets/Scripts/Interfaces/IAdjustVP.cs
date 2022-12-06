using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustVP
{
    public Faction faction { get; }
    public int vp { get; }
}
