using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class RevealWarTilesPhase : MonoBehaviour, IPhaseAction
{
    public Dictionary<Game.Faction, warBonus> warTileBonusActions = new Dictionary<Game.Faction, warBonus>();

    public void Do(Phase phase, UnityAction callback)
    {
        Theater theater = (Theater)phase;

        // We need to pass the callback to our WarTiles so that we can wait out their 

        theater.warTiles.ForEach(warTile => {
            Debug.Log($"{warTile.faction} reveals {warTile} in {theater}"); 
            theater.theaterScore[warTile.faction] += warTile.value;
        });

        theater.warTiles.ForEach(warTile => {
            theater.warTiles.Remove(warTile); 
            
            if (warTile.debt)
            {
                theater.gameActions.Add(new AdjustDebt(warTile.opposingFaction, 1));
                warTileBonusActions[warTile.opposingFaction].debt++;
            }
            else if (warTile.milDamage)
            {
                DamageMilSpace damageMilSpace = new GameObject().AddComponent<DamageMilSpace>();
                damageMilSpace.transform.parent = Player.players[warTile.faction].transform.parent;
                warTileBonusActions[warTile.faction].milDamage++;
            }
            else if (warTile.unflag)
            {
                // This is a Shift Space Action             
                //Unflag unflag = new GameObject().AddComponent<Unflag>();
                //unflag.transform.parent = Player.players[warTile.faction].transform.parent; 
                //warTileBonusActions[warTile.faction].unflag++;
            }
        }); 

        foreach (Game.Faction faction in theater.theaterScore.Keys)
        {
            Debug.Log($"{faction} has {theater.theaterScore[faction]} strength in {theater}, will incur {warTileBonusActions[faction].debt}, " +
                $"unflag {warTileBonusActions[faction].unflag} Markets, and damage {warTileBonusActions[faction].milDamage} Forts or Fleets"); 
        }
    }

    public class warBonus
    {
        public int debt = 0;
        public int milDamage = 0;
        public int unflag = 0; 
    }
}