using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class ScoreGlobalDemandAction : GameAction
    {
        protected override Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                foreach (Resource resource in peaceTurn.globalDemandResources)
                {
                    Faction resourceWinningFaction = null;

                    int britainCount = Game.Spaces.OfType<Market>().Count(market => market.control == Game.Britain && market.Resource == resource);
                    int franceCount = Game.Spaces.OfType<Market>().Count(market => market.control == Game.France && market.Resource == resource);

                    if (britainCount > franceCount)
                        resourceWinningFaction = Game.Britain;
                    else if (franceCount > britainCount)
                        resourceWinningFaction = Game.France;

                    if (resourceWinningFaction != null)
                    {
                        throw new System.NotImplementedException();
                        // commands.Push(new AdjustVPCommand(resourceWinningFaction, demandTrack[peaceTurn.era][resource][ActionPoint.ActionType.VictoryPoint])); 
                        // commands.Push(new AdjustTPCommand(resourceWinningFaction, demandTrack[peaceTurn.era][resource][ActionPoint.ActionType.Treaty])); 
                        // commands.Push(new AdjustDebtCommand(resourceWinningFaction, demandTrack[peaceTurn.era][resource][ActionPoint.ActionType.Debt])); 
                    }
                }
            }

            return Task.CompletedTask; 
        }
    }
}