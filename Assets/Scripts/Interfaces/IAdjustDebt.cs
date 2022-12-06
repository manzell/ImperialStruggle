using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdjustDebt
{
    public Faction faction { get; }
    int debt { get; }
}
