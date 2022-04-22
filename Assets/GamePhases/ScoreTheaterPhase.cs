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

        }

        originalCallback.Invoke();
    }
}
