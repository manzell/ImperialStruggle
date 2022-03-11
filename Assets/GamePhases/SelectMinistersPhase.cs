using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector; 

public class SelectMinistersPhase : SerializedMonoBehaviour, IPhaseAction
{
    public static UnityEvent<SelectMinistersPhase> selectMinisterPhaseEvent = new UnityEvent<SelectMinistersPhase>();
    public static UnityEvent<SelectMinistersPhase, Game.Faction, List<MinistryCard>> selectMinistersEvent = new UnityEvent<SelectMinistersPhase, Game.Faction,List<MinistryCard>>();
    public static UnityEvent<SelectMinistersPhase> endMinisterPhaseEvent = new UnityEvent<SelectMinistersPhase>();

    public int numMinisters = 2;
    public Dictionary<Game.Faction, List<MinistryCard>> selectedMinisters = new Dictionary<Game.Faction, List<MinistryCard>>(); 
    UnityAction callback; 

    public void Do(Phase phase, UnityAction callback)
    {
        Debug.Log("Minister Phase");
        selectMinisterPhaseEvent.Invoke(this); 

        foreach(Player player in Game.players)
        {
            player.ministers.Clear(); 

            List<MinistryCard> availableMinisters = FindObjectsOfType<MinistryCard>()
                .Where(card => card.faction == player.faction && card.eras.Contains(Phase.currentPhase.era))
                .OrderBy(card => card.name).ToList();

            // Present the player with a list of options
            
        }

        this.callback = callback; 
    }

    [Button(Name = "Select 2 Ministers")]
    public void Select(Game.Faction faction, List<MinistryCard> ministers)
    {
        if (ministers.Count != numMinisters) return; 

        Player player = Player.players[faction];
        player.ministers = ministers;

        selectedMinisters[faction] = ministers;
        selectMinistersEvent.Invoke(this, faction, player.ministers);
        
        Debug.Log($"{faction} selects {player.ministers[0]} and {player.ministers[1]} as Ministers");
        bool ready = true;

        foreach (Player p in Game.players)
            ready &= p.ministers.Count == numMinisters; // If Count ever equals 0, this will trip to false and stay there until the next Select() call

        if (ready)
        {
            endMinisterPhaseEvent.Invoke(this); 
            callback.Invoke();
        }
    }
}