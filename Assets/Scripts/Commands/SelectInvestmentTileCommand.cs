using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class SelectInvestmentTileCommand : Command
    {
        InvestmentTile tile;
        Player player; 

        public SelectInvestmentTileCommand(Player player, InvestmentTile tile)
        {
            this.tile = tile;
            this.player = player; 
        }

        public override void Do(GameAction action)
        {
            if (Phase.CurrentPhase is ActionRound actionRound)
            {
                actionRound.investmentTile = tile;
                actionRound.player.ActionPoints.Credit(tile.majorActionPoint);
                actionRound.player.ActionPoints.Credit(tile.minorActionPoint);
                player.ActionPoints.AdjustAPEvent.Invoke();
            }
        }
    }
}