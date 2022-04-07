using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordBonus : Conditional
{
    public Game.Keyword keyword;

    public override bool Test(Game.Faction faction)
    {
        Player player = Player.players[faction]; 
        return player.keywords.Contains(keyword); 
    }
}
