using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTreatyPointAction : PlayerAction, IAdjustAP, IAdjustTP
{
    public ActionPoints actionPointAward;
    public ActionPoints actionPoints => actionPointAward;
    public Game.Faction faction => player.faction;
    public int tp => -1;
    Player IAdjustAP.player => player;

}
