using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace ImperialStruggle
{
    public class CardinalMinistersAction : MinisterAction
    {
        [SerializeField] int maxDP = 3;
        [SerializeField] List<SpaceData> bonusSpaces;

        public override void Reveal(Player player)
        {
            Game.selectInvestmentTileEvent += GrantBonusDP;
        }

        void GrantBonusDP(Player selectingPlayer, InvestmentTile tile)
        {
            int BonusDP = Mathf.Min(maxDP, Game.SpaceLookup.Count(kvp => bonusSpaces.Contains(kvp.Key) && kvp.Value.Flag == Game.France));

            List<ActionPoint> aps = new() { tile.majorActionPoint, tile.minorActionPoint };
            ActionPoint AP = aps.FirstOrDefault(ap => ap.type == ActionPoint.ActionType.Diplomacy);

            if(AP != null)
            {
                Debug.Log("Cardinal Ministers Bonus Conferred");
                ActionPoint ap = new(AP.tier, ActionPoint.ActionType.Diplomacy, BonusDP);
                Commands.Push(new AddActionPointCommand(Player, ap)); 
                Exhausted = true;
            }
        }
    }
}