using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To Take Debt, we must have 1 Available Debt, We Increase our Debt by 1 and add a Debt Action Point? 
public class TakeDebtAction : PlayerAction, IAdjustDebt, IAdjustAP
{
    public ActionPoints aps;

    Player IAdjustAP.player => player;
    public Game.Faction faction => player.faction; 
    public int debt => 1;
    public ActionPoints actionPoints => aps;
}
