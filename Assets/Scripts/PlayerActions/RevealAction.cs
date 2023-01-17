using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class RevealAction : PlayerAction
    {
        MinistryCard card;

        public RevealAction(MinistryCard minister)
        {
            Name = "Reveal " + minister.Name;
            card = minister; 
        }

        protected override Task Do(IAction context)
        {
            Debug.Log($"{Player.Name} reveals {card.Name}");
            Commands.Push(new RevealCommand(Player, card)); 
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

        public override void Do(IAction action)
        {
            card.SetMinistryCardStatus(MinistryCard.MinistryCardStatus.Revealed);

            foreach(MinisterAction ministerAction in card.data.MinisterActions)
                ministerAction.Reveal(player); 
        }
    }
}
