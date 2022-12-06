using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScoreGlobalDemandAction : GameAction
{
    public Dictionary<Resource, Faction> globalDemandWinners = new ();

    protected override void Do()
    {
        if(Phase.CurrentPhase is PeaceTurn peaceTurn)
        {
            Game.Era era = GetComponent<Phase>().era; 

            foreach(Resource resource in peaceTurn.globalDemandResources)
            {
                // We don't need to know the resource - just the VP, Treaty Points, and Debt awards. 
                // We're going to set them (AND the faction). 

                Faction resourceWinningFaction = null; 

                int britainCount = Game.Markets.Where(market => market.flag == Game.Britain && market.marketType == resource).Count();
                int franceCount = Game.Markets.Where(market => market.flag == Game.France && market.marketType == resource).Count();

                if (britainCount > franceCount)
                    resourceWinningFaction = Game.Britain;
                else if (franceCount > britainCount)
                    resourceWinningFaction = Game.France;


                if (resourceWinningFaction != null)
                {
                    globalDemandWinners.Add(resource, resourceWinningFaction);

                    throw new System.NotImplementedException();
                    // commands.Push(new AdjustVPCommand(resourceWinningFaction, demandTrack[era][resource][ActionPoint.ActionType.VictoryPoint])); 
                    // commands.Push(new AdjustTPCommand(resourceWinningFaction, demandTrack[era][resource][ActionPoint.ActionType.Treaty])); 
                    // commands.Push(new AdjustDebtCommand(resourceWinningFaction, demandTrack[era][resource][ActionPoint.ActionType.Debt])); 
                }
            }
        }
    }
}