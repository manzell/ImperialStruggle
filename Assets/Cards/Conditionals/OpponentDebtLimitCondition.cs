using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentDebtLimitCondition : Conditional
{
    [SerializeField] int margin = 1;
    public override bool Test(Game.Faction faction)
    {
        RecordsTrack recordsTrack = FindObjectOfType<RecordsTrack>();
        Game.Faction opposingFaction = faction == Game.Faction.England ? Game.Faction.France : Game.Faction.England; 

        if(margin >= 0)
            return recordsTrack.availableDebt[opposingFaction] >= margin;
        else
            return recordsTrack.availableDebt[opposingFaction] == margin;
    }
}
