using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustTPCommand : Command
{
    public static UnityEvent<Game.Faction, int> adjustTPEvent = new UnityEvent<Game.Faction, int>();
    public Calculation<int> adjustAmount;
    int previousTP;

    public override void Do(Game.Faction faction)
    {
        int val = adjustAmount.value; 
        if(targetFaction == Game.Faction.Neutral)
            targetFaction = faction;

        Debug.Log($"{targetFaction} treaty points {(val > 0 ? "increased" : "decreased")} by {Mathf.Abs(val)}");
        FindObjectOfType<RecordsTrack>().treatyPoints[targetFaction] += val;
        adjustTPEvent.Invoke(targetFaction, val); 
    }

    public override void Undo()
    {
        FindObjectOfType<RecordsTrack>().treatyPoints[targetFaction] = previousTP;
        adjustTPEvent.Invoke(targetFaction, adjustAmount.value);
    }
}