using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ScoreGlobalDemandPhase : MonoBehaviour, IPhaseAction
{
    public void Do(Phase phase, UnityAction callback)
    {
        GlobalDemandTrack track = FindObjectOfType<GlobalDemandTrack>();

        foreach(Game.Resource resource in (phase as PeaceTurn).globalDemandResources)
        {
            Dictionary<Game.Faction, int> demandScore = new Dictionary<Game.Faction, int>() {
                { Game.Faction.Britain, FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.Britain).Count() },
                { Game.Faction.France, FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.France).Count() }
            };

            Game.Faction winningFaction = Game.Faction.Neutral;  
            if(demandScore[Game.Faction.France] > demandScore[Game.Faction.Britain]) winningFaction = Game.Faction.France;
            else if (demandScore[Game.Faction.Britain] > demandScore[Game.Faction.France]) winningFaction = Game.Faction.Britain;

            foreach (Game.ActionType action in track.globalDemandAwards[(phase.era, resource)].Keys)
            {
                switch (action)
                {
                    case Game.ActionType.VictoryPoint:
                        AdjustVPCommand adjustVictoryPoints = phase.gameObject.AddComponent<AdjustVPCommand>();
                        adjustVictoryPoints.adjustAmount.value = track.globalDemandAwards[(phase.era, resource)][action];
                        adjustVictoryPoints.targetFaction = winningFaction;
                        adjustVictoryPoints.Do(winningFaction);
                        break;
                    case Game.ActionType.Treaty:

                        AdjustTPCommand adjustTPCommand = gameObject.AddComponent<AdjustTPCommand>();
                        adjustTPCommand.targetFaction = winningFaction;
                        adjustTPCommand.adjustAmount.value = track.globalDemandAwards[(phase.era, resource)][action];
                        adjustTPCommand.Do(winningFaction);
                        break;
                    case Game.ActionType.Debt:
                        AdjustDebtCommand adjustDebt = phase.gameObject.AddComponent<AdjustDebtCommand>();
                        adjustDebt.targetFaction = winningFaction;
                        adjustDebt.adjustAmt = track.globalDemandAwards[(phase.era, resource)][action];
                        adjustDebt.Do(winningFaction); 
                        break;
                }
            }
        }
    }
}