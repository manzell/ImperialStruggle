using UnityEngine;

public class DealCardsAction : GameAction
{
    enum DealType { UpTo, Fixed }
    [SerializeField] DealType dealType;
    [SerializeField] int numCards;

    protected override void Do()
    {
        for(int i = 0; i < numCards; i++)
            foreach (Player player in Player.players)
                if(dealType == DealType.Fixed || (dealType == DealType.UpTo && player.hand.Count < numCards))
                    commands.Add(new DealCardCommand(player, 1)); 
    }
}