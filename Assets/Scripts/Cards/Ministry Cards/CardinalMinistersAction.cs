using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class CardinalMinistersAction : MinisterAction
    {
        [SerializeField] int maxDP = 3;
        [SerializeField] List<SpaceData> bonusSpaces;

        public override void Reveal(Player player)
        {
            Game.selectInvestmentTileEvent += GrantBonusDP;

            // If the only thing the player has done is select their Investment Tile, they can also get the bonus
            if (Phase.CurrentPhase is ActionRound ar && ar.ExecutedCommands.Last() is SelectInvestmentTileCommand)
                GrantBonusDP(player, ar.investmentTile); 
        }
        protected override void Retire(Player player) => Game.selectInvestmentTileEvent += GrantBonusDP;

        void GrantBonusDP(Player selectingPlayer, InvestmentTile tile)
        {
            int BonusDP = Mathf.Min(maxDP, Game.SpaceLookup.Count(kvp => bonusSpaces.Contains(kvp.Key) && kvp.Value.Flag == Game.France));

            List<ActionPoint> aps = new() { tile.majorActionPoint, tile.minorActionPoint };
            ActionPoint AP = aps.FirstOrDefault(ap => ap.type == ActionPoint.ActionType.Diplomacy);

            if(AP != null)
            {
                Debug.Log("Cardinal Ministers Bonus Conferred");
                ActionPoint ap = new(AP.tier, ActionPoint.ActionType.Diplomacy, BonusDP);
                Commands.Push(new AddAPCommand(selectingPlayer, ap)); 
                Exhausted = true;
            }
        }
    }
}