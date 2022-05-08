using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreAvailableDebtCondition : Conditional
{
    [SerializeField] int margin = 1;
    public override bool Test(BaseAction context)
    {
        if (context is PlayerAction playerAction) 
        {
            Player player = playerAction.player; 
            Dictionary<Game.Faction, int> availableDebt = GameObject.FindObjectOfType<RecordsTrack>().availableDebt;
            Game.Faction opposingFaction = player.faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

            return margin == 0 ? availableDebt[player.faction] == availableDebt[opposingFaction] : 
                availableDebt[player.faction] - availableDebt[opposingFaction] >= margin;
        }
        return true; 
    }
}