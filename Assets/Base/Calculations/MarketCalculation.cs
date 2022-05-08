using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class MarketCalculation : Calculation<List<Space>>
{
    [SerializeField] List<Game.Resource> marketTypes;

    public override List<Space> Calculate() => 
        FindObjectsOfType<Space>().Where(market => market is Market && marketTypes.Contains((market as Market).marketType)).ToList(); 
}
