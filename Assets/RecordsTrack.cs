using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 

public class RecordsTrack : SerializedMonoBehaviour
{
    public Dictionary<Game.Faction, int> debtLimit;
    public Dictionary<Game.Faction, int> currentDebt;
    public Dictionary<Game.Faction, int> treatyPoints;
    public int VictoryPoints; 

    public Dictionary<Game.Faction, int> availableDebt
    {
        get
        {
            Dictionary<Game.Faction, int> retVal = new Dictionary<Game.Faction, int>(); 

            foreach(Game.Faction faction in debtLimit.Keys)
                retVal.Add(faction, debtLimit[faction] - currentDebt[faction]); 

            return retVal; 
        }
    }
}
