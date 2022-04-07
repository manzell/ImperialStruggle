using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AdjustTreatyPoints : Command
{
    public static UnityEvent<Game.Faction, int> adjustTPevent = new UnityEvent<Game.Faction, int>();
    public int adjustAmount;
    int previousTP;
    public RecordsTrack recordsTrack;

    public AdjustTreatyPoints(Game.Faction faction, int adjustAmount)
    {
        recordsTrack = GameObject.FindObjectOfType<RecordsTrack>();
        actingFaction = faction;
        this.adjustAmount = adjustAmount;
        previousTP = recordsTrack.treatyPoints[faction];
        Do(); 
    }

    public void Do()
    {
        Debug.Log($"{actingFaction} treaty points {(adjustAmount > 0 ? "increased" : "decreased")} by {Mathf.Abs(adjustAmount)}");
        recordsTrack.treatyPoints[actingFaction] += adjustAmount;
        adjustTPevent.Invoke(actingFaction, adjustAmount); 
    }

    public override void Undo()
    {
        recordsTrack.treatyPoints[actingFaction] = previousTP;
    }
}