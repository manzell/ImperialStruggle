using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermanDiplomacyAction : PlayerAction, IAdjustAP
{
    public ActionPoints awardAP = new ActionPoints(); 

    public ActionPoints actionPoints => awardAP;
    Player IAdjustAP.player => player;
}
