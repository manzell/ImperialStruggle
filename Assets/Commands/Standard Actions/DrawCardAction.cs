using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardAction : Action
{

    public override bool Can(Game.Faction faction)
    {
        return Game.CanAfford(_actionCost, Player.players[faction].actionPoints); 
    }

    public override void Do(Game.Faction faction)
    {
        Player player = Player.players[faction];

        if(Can(faction))
        {
            Phase.currentPhase.gameActions.Add(new AdjustActionPoints(faction, _actionCost));
            Phase.currentPhase.gameActions.Add(new DrawCard(player));
        }
    }
}
