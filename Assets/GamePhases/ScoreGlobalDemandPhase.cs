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
                { Game.Faction.England, FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.England).Count() },
                { Game.Faction.France, FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.France).Count() }
            };

            Game.Faction winningFaction = Game.Faction.Neutral;  
            if(demandScore[Game.Faction.France] > demandScore[Game.Faction.England]) winningFaction = Game.Faction.France;
            else if (demandScore[Game.Faction.England] > demandScore[Game.Faction.France]) winningFaction = Game.Faction.England;

            foreach (Game.ActionType action in track.globalDemandAwards[(phase.era, resource)].Keys)
            {
                switch (action)
                {
                    case Game.ActionType.VictoryPoint:
                        phase.gameActions.Add(new AdjustVictoryPoints(winningFaction, track.globalDemandAwards[(phase.era, resource)][action]));
                        break;
                    case Game.ActionType.Treaty:
                        phase.gameActions.Add(new AdjustTreatyPoints(winningFaction, track.globalDemandAwards[(phase.era, resource)][action]));
                        break;
                    case Game.ActionType.Debt:
                        phase.gameActions.Add(new AdjustDebt(winningFaction, track.globalDemandAwards[(phase.era, resource)][action]));
                        break;
                }
            }
        }
    }
}