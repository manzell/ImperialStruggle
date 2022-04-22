using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 

public class WarPreparationPhase : MonoBehaviour
{
    public Phase warPhase; 

    public void Do(Phase phase, UnityAction callback)
    {
        foreach(Theater theater in warPhase.GetComponentsInChildren<Theater>())
        {
            foreach(Player player in Game.players)
            {
                int i = Random.Range(0, player.basicWarTiles.Count); 

                WarTile tile = player.basicWarTiles[i];

                while(theater.warTiles.Contains(tile))
                    tile = player.basicWarTiles[Random.Range(0, player.basicWarTiles.Count)];

                //phase.gameActions.Add(new AddWarTile(tile, theater)); 
            }
        }

        callback.Invoke(); 
    }
}