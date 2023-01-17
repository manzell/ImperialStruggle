using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Utilities;

namespace ImperialStruggle
{
    public class AdvantageTile : ISelectable
    {
        public string Name => Data.Name;
        public AdvantageData Data; 
        public bool Exhausted = false; 

        [field: SerializeField] public HashSet<Space> adjacentSpaces { get; private set; }

        public Faction faction => adjacentSpaces.All(space => space.Flag == adjacentSpaces.First().Flag) ? 
            adjacentSpaces.First().Flag : null;

        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public AdvantageTile(AdvantageData data)
        {
            Data = data;
            data.adjacentSpaces.ForEach(data => adjacentSpaces.Add(Game.SpaceLookup[data])); 
        }

        public async void Do(Player player)
        {
            foreach(PlayerAction action in Data.playerActions)
            {
                action.SetPlayer(player);
                await action.Execute(); 
            }
        }
    }
}