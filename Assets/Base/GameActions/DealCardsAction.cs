using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Collections; 

public class DealCardsAction : GameAction
{
    enum DealType { UpTo, Fixed }
    [SerializeField] DealType dealType;
    [SerializeField] int numCards;
    [SerializeField] List<Game.Faction> factions = new List<Game.Faction>();

    [ReadOnly] public Game.Faction faction; 

    protected override void Do(UnityAction callback)
    {
        for(int i = 0; i < numCards; i++)
        {
            foreach (Game.Faction _faction in factions)
            {
                faction = _faction;
                Player player = Player.players[_faction];

                switch (dealType)
                {
                    case DealType.UpTo:
                        if (player.hand.Count < numCards)
                            base.Do(() => { });
                        break;
                    case DealType.Fixed:
                        base.Do(() => { });
                        break;
                }
            }
        }

        onActionEvent.Invoke(this);
        callback.Invoke(); 
    }
}