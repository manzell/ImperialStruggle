using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtLimitCondition : Conditional
{
    enum LimitConditionType { AtLeast, Exactly }
    [SerializeField] Game.Faction targetFaction; 
    [SerializeField] int margin = 1;
    [SerializeField] LimitConditionType limitConditionType;

    // Does the Faction of the given Player have at least margin available debt? 
    public override bool Test(Object player)
    {
        if (player is Player)
        {
            Dictionary<Game.Faction, int> availableDebt = GameObject.FindObjectOfType<RecordsTrack>().availableDebt;
            Player _player = (Player)player;
            Game.Faction faction = _player.faction;
            Game.Faction opposingFaction = faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

            if (limitConditionType == LimitConditionType.Exactly)
                return availableDebt[opposingFaction] == margin;
            else //if(limitConditionType == LimitConditionType.AtLeast)
                return availableDebt[opposingFaction] >= margin;
        }
        return true; 
    }
}
