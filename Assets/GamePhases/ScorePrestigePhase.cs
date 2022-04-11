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
        Dictionary<Game.Faction, int> prestigeScore = new Dictionary<Game.Faction, int>() { { Game.Faction.Britain, 0 }, { Game.Faction.France, 0 } };

        List<Space> prestigeSpaces = FindObjectsOfType<Space>().Where(space => space.prestige).ToList();

        prestigeSpaces.ForEach(space => { 
            if(prestigeScore.ContainsKey(space.flag))
                prestigeScore[space.flag]++;
        });

        AdjustVPCommand adjustVictoryPoints = phase.gameObject.AddComponent<AdjustVPCommand>();
        adjustVictoryPoints.adjustAmount.value = prestigeVPAward;

        if (prestigeScore[Game.Faction.Britain] > prestigeScore[Game.Faction.France])
            adjustVictoryPoints.targetFaction = Game.Faction.Britain; 
        else if(prestigeScore[Game.Faction.France] > prestigeScore[Game.Faction.Britain])
            adjustVictoryPoints.targetFaction = Game.Faction.France;

        phase.gameActions.Add(adjustVictoryPoints);
    }
}
