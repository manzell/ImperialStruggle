using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreAvailableDebtCondition : Conditional
{
    [SerializeField] int margin = 1;

    public Conditional.ConditionType ConditionalType => Conditional.ConditionType.MoreThan;
    public string ConditionalText => "Available Debt Condition"; 

    public bool Test(GameAction context)
    {
        if (context is PlayerAction playerAction) 
        {
            Player player = playerAction.actingPlayer; 
            Dictionary<Faction, int> availableDebt = RecordsTrack.availableDebt;
            Faction opposingFaction = player.faction == Game.Britain ? Game.France : Game.Britain;

            return margin == 0 ? availableDebt[player.faction] == availableDebt[opposingFaction] : 
                availableDebt[player.faction] - availableDebt[opposingFaction] >= margin;
        }
        return true; 
    }
}