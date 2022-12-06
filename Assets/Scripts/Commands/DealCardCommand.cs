using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class DealCardCommand : Command
{
    public static UnityEvent<EventCard> dealCardEvent = new UnityEvent<EventCard>();
    [SerializeField] Player player;
    [SerializeField] int num = 1;

    List<EventCard> dealtCards; 

    public DealCardCommand(Player player, int num)
    {
        this.player = player;
        this.num = num; 
    }

    public override void Do(GameAction action)
    {
        for(int i = 0; i < num; i++)
        {
            EventCard card = Game.eventDeck.First();
            Game.eventDeck.Remove(card);
            player.hand.Add(card);
            dealtCards.Add(card);
            dealCardEvent.Invoke(card);

            Game.Log($"{card} dealt to {player.name}");
        }
    }

    public override void Undo()
    {
        foreach(EventCard card in dealtCards)
        {
            player.hand.Remove(card);
            Game.eventDeck.Add(card); 
        } 
    }
}
