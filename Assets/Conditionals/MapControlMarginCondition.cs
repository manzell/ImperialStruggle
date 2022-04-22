using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MapControlMarginCondition : Conditional
{
    [SerializeField] int requiredMargin = 2; 

    public override bool Test(Object _map)
    {
        if (_map is Map)
        {
            Map map = (Map)_map;
            return Mathf.Abs(GameObject.FindObjectsOfType<Space>().Where(space => space.map == map && space.flag == Game.Faction.Britain).Count() -
                GameObject.FindObjectsOfType<Space>().Where(space => space.map == map && space.flag == Game.Faction.France).Count()) >= requiredMargin;
        }
        return true; 
    }
}
