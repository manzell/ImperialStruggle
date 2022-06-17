using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Collections; 

public class DealCardsAction : GameAction, ITargetType<Player>
{
    enum DealType { UpTo, Fixed }
    [SerializeField] DealType dealType;
    [SerializeField] int numCards;
    [SerializeField] List<Game.Faction> factions = new List<Game.Faction>();

    Player player;
    public Player target => player;

    public override void Do(UnityAction callback)
    {
        for(int i = 0; i < numCards; i++)
        {
            foreach (Game.Faction _faction in factions)
            {
                player = Player.players[_faction];

                if(dealType != DealType.UpTo || player.hand.Count < numCards)
                    base.Do(() => { });
            }
        }

        callback.Invoke(); 
    }
}