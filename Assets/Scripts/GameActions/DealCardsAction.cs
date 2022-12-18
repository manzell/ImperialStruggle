using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class DealCardsAction : GameAction
    {
        enum DealType { UpTo, Fixed }
        [SerializeField] DealType dealType;
        [SerializeField] int numCards;

        protected override Task Do()
        {
            for (int i = 0; i < numCards; i++)
                foreach (Player player in Player.Players)
                    if (dealType == DealType.Fixed || (dealType == DealType.UpTo && player.Cards.Count < numCards))
                        Commands.Push(new DealCardCommand(player));

            return Task.CompletedTask; 
        }
    }
}