using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class ActionRound : Phase
    {
        public static System.Action<ActionRound> ActionRoundStartEvent, ActionRoundEndEvent;
        public Player player;
        public InvestmentTile investmentTile;
        bool actionRoundCompleted;

        // The Action Round only completes when the player signals that they have completed it via a Button Press
        public override bool Completed => actionRoundCompleted;

        public override void StartPhase()
        {
            ActionRoundStartEvent?.Invoke(this); 
            base.StartPhase();
        }

        public void Setup(Player player)
        {
            this.player = player;
            foreach (PlayerAction action in PhaseStartActions)
                action.SetPlayer(player); 
        }
    }
}