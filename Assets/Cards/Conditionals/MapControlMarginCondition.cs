using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MapControlMarginCondition : Conditional<Map>
{
    [SerializeField] int requiredMargin = 2; 

    public override bool Test(Map map)
    {   
        return Mathf.Abs(FindObjectsOfType<Space>().Where(space => space.map == map && space.flag == Game.Faction.Britain).Count() -
            FindObjectsOfType<Space>().Where(space => space.map == map && space.flag == Game.Faction.France).Count()) >= requiredMargin;
    }
}
