using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class RevealAction : PlayerAction
    {
        MinistryCard card;
        Player player; 

        public RevealAction(MinistryCard minister, Player player)
        {
            Name = "Reveal " + minister.Name;
            card = minister; 
            this.player = player;

            Setup(player);
        }

        protected override Task Do()
        {
            Debug.Log($"{Player.Name} reveals {card.Name}");
            Commands.Push(new RevealCommand(player, card)); 
            return Task.CompletedTask; 
        }
    }

    public class RevealCommand : Command
    {
        Player player; 
        MinistryCard card;
        public RevealCommand(Player player, MinistryCard card)
        {
            this.card = card;
            this.player = player;
        }

        public override void Do(GameAction action)
        {
            card.ministryCardStatus = MinistryCard.MinistryCardStatus.Revealed;
            foreach(MinisterAction ministerAction in card.data.MinisterActions)
            {
                ministerAction.Reveal(); 
            }
        }
    }
}
