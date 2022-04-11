using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class WarTile : MonoBehaviour, IPhaseAction
{
    public int value;
    public bool debt, milDamage, unflag; 
    public Game.Faction faction;
    public Game.Faction opposingFaction => faction == Game.Faction.Britain ? Game.Faction.France : Game.Faction.Britain;

    public void Do(Phase phase, UnityAction callback)
    {
        Debug.Log($"{this} revealed for {faction} in {phase}");

        if (unflag)
        {
            Debug.Log("Unflag Tile Triggered");
        }
        else if (milDamage)
        {
            Debug.Log("Whatever");
        }
        else
        {
            callback.Invoke(); 
        }
    }


}
