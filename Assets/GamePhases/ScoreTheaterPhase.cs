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
        int winningMargin = Mathf.Abs(theater.theaterScore[Game.Faction.England] - theater.theaterScore[Game.Faction.France]);
        Game.Faction winningFaction = theater.theaterScore[Game.Faction.France] > theater.theaterScore[Game.Faction.England] ? Game.Faction.France :
            theater.theaterScore[Game.Faction.England] > theater.theaterScore[Game.Faction.France] ? Game.Faction.England :
            Game.Faction.Neutral;
        Game.Faction losingFaction = winningFaction == Game.Faction.France ? Game.Faction.England :
            winningFaction == Game.Faction.England ? Game.Faction.France :
            Game.Faction.Neutral;

        foreach (TheaterAwards award in theater.theaterAwards)
        {
            if (winningMargin >= award.minMargin && (winningMargin <= award.maxMargin || award.maxMargin == 0))
            {
                if (award.vpAward > 0)
                    theater.gameActions.Add(new AdjustVictoryPoints(winningFaction, award.vpAward));

                if (award.cpAward > 0)
                    theater.gameActions.Add(new AdjustConquestPoints(winningFaction, award.cpAward));

                if (award.loserTreatyPoints > 0)
                    theater.gameActions.Add(new AdjustTreatyPoints(losingFaction, award.loserTreatyPoints));

                break;
            }
        }

        originalCallback.Invoke();
    }
}
