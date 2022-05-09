using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordCondition : Conditional
{
    public Game.Keyword keyword;

    // Checks to see if the ActivePlayer has the keyword on them
    public override bool Test(BaseAction action)
    {
        if (action is PlayerAction playerAction)
            return playerAction.player.keywords.Contains(keyword);
        else
            return true; 
    }
}
