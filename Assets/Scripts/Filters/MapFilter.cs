using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MapFilter : Conditional<Space>
    {
        [SerializeField] List<Map> maps;

        protected override bool Test(Space context) => maps.Contains(context.map); 
    }
}
