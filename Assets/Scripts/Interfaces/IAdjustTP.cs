using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustTP
{
    public Faction faction { get; }
    int tp { get; }
}
