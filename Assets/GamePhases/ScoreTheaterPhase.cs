using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class ScoreTheaterPhase : MonoBehaviour
{
    UnityAction originalCallback; 

    void GiveWarSpoils(Theater theater)
    {
        int winningMargin = Mathf.Abs(theater.theaterScore[Game.Faction.Britain] - theater.theaterScore[Game.Faction.France]);
        Game.Faction winningFaction = theater.theaterScore[Game.Faction.France] > theater.theaterScore[Game.Faction.Britain] ? Game.Faction.France :
            theater.theaterScore[Game.Faction.Britain] > theater.theaterScore[Game.Faction.France] ? Game.Faction.Britain :
            Game.Faction.Neutral;
        Game.Faction losingFaction = winningFaction == Game.Faction.France ? Game.Faction.Britain :
            winningFaction == Game.Faction.Britain ? Game.Faction.France :
            Game.Faction.Neutral;

        foreach (TheaterAwards award in theater.theaterAwards)
        {
            if (winningMargin >= award.minMargin && (winningMargin <= award.maxMargin || award.maxMargin == 0))
            {
                if (award.vpAward > 0)
                {
                    AdjustVPCommand adjustVictoryPoints = theater.gameObject.AddComponent<AdjustVPCommand>();
                    adjustVictoryPoints.adjustAmount.value = award.vpAward;
                    adjustVictoryPoints.targetFaction = winningFaction;
                    adjustVictoryPoints.Do(winningFaction); 
                }

                if (award.cpAward > 0)
                    theater.gameActions.Add(new AdjustConquestPoints(winningFaction, award.cpAward));

                if (award.loserTreatyPoints > 0)
                {
                    AdjustTPCommand adjustTPCommand = gameObject.AddComponent<AdjustTPCommand>();
                    adjustTPCommand.targetFaction = losingFaction;
                    adjustTPCommand.adjustAmount.value = award.loserTreatyPoints;
                    adjustTPCommand.Do(winningFaction);
                }

                break;
            }
        }

        originalCallback.Invoke();
    }
}
