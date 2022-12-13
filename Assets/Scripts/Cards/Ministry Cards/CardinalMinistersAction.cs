using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class CardinalMinistersAction : PlayerAction
    {
        [SerializeField] int maxDP = 3;
        [SerializeField] List<SpaceData> bonusSpaces;

        bool used;

        protected override void Do() // NOTE: Do is the REVEAL action, 
        {
            PeaceTurn peaceTurn = Phase.CurrentPhase.GetComponentInParent<PeaceTurn>();

            peaceTurn.EndEvent += Reset;
            Game.selectInvestmentTileEvent += GrantBonusDP;

            void GrantBonusDP(Player selectingPlayer, InvestmentTile tile)
            {
                List<ActionPoint> aps = new() { tile.majorActionPoint, tile.minorActionPoint }; 
                ActionPoint AP = aps.FirstOrDefault(ap => ap.actionType == ActionPoint.ActionType.Diplomacy);

                if (selectingPlayer == Game.France && AP != null && used == false)
                {
                    int BonusDP = Mathf.Min(maxDP, Game.SpaceLookup.Count(kvp => bonusSpaces.Contains(kvp.Key) && kvp.Value.Flag == Game.France));
                    AP.baseValue += BonusDP;
                    used = true;
                }
            }

            void Reset() => used = false;
        }
    }
}