using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using System;

namespace ImperialStruggle
{
    public abstract class AdvantageTile // : PlayerAction, ISelectable
    {
        public bool Exhausted = false; 

        [SerializeField] HashSet<SpaceData> adjacentSpaces;

        public Faction faction => adjacentSpaces.All(space => Game.SpaceLookup[space].Flag == Game.SpaceLookup[adjacentSpaces.First()].Flag) ? 
            Game.SpaceLookup[adjacentSpaces.First()].Flag : null;

        public Action UISelectionEvent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action UIDeselectEvent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}