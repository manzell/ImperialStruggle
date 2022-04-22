using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordCondition : Conditional
{
    public Game.Keyword keyword;

    public override bool Test(Object player)
    {
        if (player is Player)
            return (player as Player).keywords.Contains(keyword);
        return true;         
    }
}
