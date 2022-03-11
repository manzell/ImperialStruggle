using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public AwardTile awardTile;
    public int bonusVP; 
    public List<Space> spaces; 

    public Dictionary<Game.Faction, int> mapScore
    {
        get
        {
            Dictionary<Game.Faction, int>  retVal = new Dictionary<Game.Faction, int> {
                { Game.Faction.England, 0 },
                { Game.Faction.France, 0 }
            }; 

            foreach(Space space in spaces)
                if(retVal.ContainsKey(space.flag))
                    retVal[space.flag]++; 

            return retVal; 
        }
    }
}
