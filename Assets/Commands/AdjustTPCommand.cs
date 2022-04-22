using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustTPCommand : Command
{
    public static UnityEvent<Game.Faction, int> adjustTPEvent = new UnityEvent<Game.Faction, int>();
    public Calculation<int> adjustAmount;
    public Game.Faction targetFaction, actingFaction; 
    int previousTP;

    public override void Do(Action action)
    {
        int val = adjustAmount.value; 
        if(targetFaction == Game.Faction.Neutral)
            targetFaction = actingFaction;

        Debug.Log($"{targetFaction} treaty points {(val > 0 ? "increased" : "decreased")} by {Mathf.Abs(val)}");
        GameObject.FindObjectOfType<RecordsTrack>().treatyPoints[targetFaction] += val;
        adjustTPEvent.Invoke(targetFaction, val); 
    }

    public override void Undo()
    {
        GameObject.FindObjectOfType<RecordsTrack>().treatyPoints[targetFaction] = previousTP;
        adjustTPEvent.Invoke(targetFaction, adjustAmount.value);
    }
}