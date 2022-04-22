using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class AwardPhase : MonoBehaviour
{
    public static UnityEvent<Map, AwardTile> SetMapAwardEvent = new UnityEvent<Map, AwardTile>();

    Dictionary<Map, AwardTile> previousAwardTiles = new Dictionary<Map, AwardTile>();

    public void Do(Phase phase, UnityAction callback)
    {
        List<AwardTile> awardTiles = FindObjectsOfType<AwardTile>().ToList();
        Map[] maps = FindObjectsOfType<Map>();
        previousAwardTiles.Clear();

        foreach (Map map in maps)
            previousAwardTiles.Add(map, map.awardTile);

        if (awardTiles.Where(tile => tile.used == false).Count() < 4)
        {
            awardTiles.ForEach(tile => tile.used = false);
            Debug.Log("Reshuffling Award Tiles"); 
        }

        IOrderedEnumerable<AwardTile> availableAwardTiles = awardTiles.Where(tile => tile.used == false).OrderBy(t => Random.value);

        for (int i = 0; i < maps.Count() && i < availableAwardTiles.Count(); i++)
        {
            maps[i].awardTile = availableAwardTiles.ElementAt(i); 
            availableAwardTiles.ElementAt(i).used = true;

            SetMapAwardEvent.Invoke(maps[i], availableAwardTiles.ElementAt(i));
            Debug.Log($"Award Tile {availableAwardTiles.ElementAt(i)} assigned to {maps[i]}");
        }

        callback.Invoke(); 
    }

    public void Undo()
    {
        foreach(Map map in previousAwardTiles.Keys)
            map.awardTile = previousAwardTiles[map];
    }
}
