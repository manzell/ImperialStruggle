using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreAvailableDebtCondition : Conditional
{
    [SerializeField] int margin = 1;
    public override bool Test(Game.Faction faction)
    {
        RecordsTrack recordsTrack = FindObjectOfType<RecordsTrack>();
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England;

        if (margin > 0)
            return recordsTrack.availableDebt[faction] - recordsTrack.availableDebt[opposingFaction] >= margin;
        else if (margin == 0)
            return recordsTrack.availableDebt[faction] == recordsTrack.availableDebt[opposingFaction];
        return false; 
    }
}