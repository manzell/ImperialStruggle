using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

[System.Serializable]
public class GlobalDemandTrack : Dictionary<Game.Era, Dictionary<Game.Resource, Dictionary<Game.ActionType, int>>>
{
    //public Dictionary<Game.Era, Dictionary<Game.Resource, int>> globalDemandAwards = new Dictionary<Game.Era,Dictionary<Game.Resource, int>>();
}
