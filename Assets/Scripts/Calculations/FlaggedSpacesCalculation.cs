using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlaggedSpacesCalculation : Calculation<List<Space>>
{
    [SerializeField] List<Space> eligibleSpaces = new List<Space>();
    [SerializeField] List<Faction> eligibleFlags = new List<Faction>();

    public override List<Space> Calculate() => eligibleSpaces.Where(space => eligibleFlags.Contains(space.flag)).ToList();
}
