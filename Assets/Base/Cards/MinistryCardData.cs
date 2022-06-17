using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using Sirenix.OdinInspector;
using System.Linq; 

[CreateAssetMenu]
public class MinistryCardData : SerializedScriptableObject, IPlayerAction
{
    public Game.Faction faction;
    public List<Game.Keyword> keywords = new List<Game.Keyword>();
    public List<Game.Era> eras;
    public string cardText;

    public Trigger trigger; 
    public List<BaseAction> actions; 

    public Player player => Player.players[faction]; 

    public void Activate() => trigger.onTrigger += Try;
    public void Deactivate() => trigger.onTrigger -= Try; 

    void Try()
    {
        foreach (BaseAction action in actions)
            action.Try(null);
    }
}