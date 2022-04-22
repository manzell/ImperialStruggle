using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EvictSquadronCommand : Command
{
    [SerializeField] Squadron squadron;

    public override void Do(Action action)
    {
        //List<Squadron> squadrons = FindObjectsOfType<Squadron>().Where(squadron => squadron.flag == targetFaction && squadron.space != null).ToList();

        // TODO - Show Select Window
    }
}
