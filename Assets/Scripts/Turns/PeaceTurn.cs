using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public class PeaceTurn : Phase
    {
        public static System.Action<PeaceTurn> StartPeaceTurnEvent, EndPeaceTurnEvent;
        public Player initiative;
        public HashSet<Resource> globalDemandResources = new();
        public Dictionary<Map, AwardTile> awardTiles = new Dictionary<Map, AwardTile>();
        public Dictionary<InvestmentTile, Faction> investmentTiles = new();

        // A Peace Turn is Completed when all Action Rounds are complete
        public override bool Completed => GetComponentsInChildren<ActionRound>().All(actionRound => actionRound.Completed);

        public override void StartPhase()
        {
            StartPeaceTurnEvent?.Invoke(this); 
            base.StartPhase();
        }

        public override void EndPhase()
        {
            EndPeaceTurnEvent?.Invoke(this);
            base.EndPhase();
        }
    }
}