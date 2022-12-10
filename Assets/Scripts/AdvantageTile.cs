using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class AdvantageTile : MonoBehaviour
    {
        public enum AdvantageTileState { Ready, Exhaused }
        public AdvantageTileState tileState { get; private set; }
        [SerializeField] HashSet<Space> adjacentSpaces = new();

        // Heads up this won't work because we can't set Spaces until runtime
        public Faction faction => adjacentSpaces.All(space => space.Flag == adjacentSpaces.First().Flag) ? adjacentSpaces.First().Flag : null;
    }
}