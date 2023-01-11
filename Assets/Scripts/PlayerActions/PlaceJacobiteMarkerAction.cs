using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class PlaceJacobiteMarkerAction : PlayerAction
    {
        [SerializeField] int requiredVictoryMargin;
        [SerializeField] Theater theater; 

        protected override Task Do()
        {
            if(theater.GetTheaterScore(Player.Faction) - theater.GetTheaterScore(Player.Opponent.Faction) >= requiredVictoryMargin &&
                Player.Faction == Game.France)
                Jacobites.VictoryMarkers++;

            return Task.CompletedTask; 
        }
    }
}
