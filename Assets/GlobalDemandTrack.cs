using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDemandTrack : MonoBehaviour
{
    public Dictionary<(Game.Era era, Game.Resource resource), Dictionary<Game.ActionType, int>> globalDemandAwards =
        new Dictionary<(Game.Era era, Game.Resource resource), Dictionary<Game.ActionType, int>>(); 
}
