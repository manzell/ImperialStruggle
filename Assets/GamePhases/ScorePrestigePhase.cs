using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScorePrestigePhase : MonoBehaviour, IPhaseAction
{
    public int prestigeVPAward = 2;

    public void Do(Phase phase, UnityAction callback)
    {
        Dictionary<Game.Faction, int> prestigeScore = new Dictionary<Game.Faction, int>() { { Game.Faction.England, 0 }, { Game.Faction.France, 0 } };

        List<Space> prestigeSpaces = FindObjectsOfType<Space>().Where(space => space.prestige).ToList();

        prestigeSpaces.ForEach(space => { 
            if(prestigeScore.ContainsKey(space.flag))
                prestigeScore[space.flag]++;
        });

        if(prestigeScore[Game.Faction.England] > prestigeScore[Game.Faction.France])
            Phase.currentPhase.gameActions.Add(new AdjustVictoryPoints(Game.Faction.England, prestigeVPAward)); 
        else if(prestigeScore[Game.Faction.France] > prestigeScore[Game.Faction.England])
            Phase.currentPhase.gameActions.Add(new AdjustVictoryPoints(Game.Faction.France, prestigeVPAward));
    }
}
