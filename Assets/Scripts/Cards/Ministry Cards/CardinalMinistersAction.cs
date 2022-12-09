using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ImperialStruggle;
using Sirenix.Utilities;

public class CardinalMinistersAction : CardAction
{
    [SerializeField] int maxDP = 3;
    [SerializeField] List<SpaceData> bonusSpaces;

    bool used; 

    protected override void Do(Player player) // NOTE: Do is the REVEAL action, 
    {
        PeaceTurn peaceTurn = Phase.CurrentPhase.GetComponentInParent<PeaceTurn>();

        peaceTurn.EndEvent += Reset; 
        InvestmentTile.selectInvestmentTileEvent += GrantBonusDP;

        void GrantBonusDP(Player selectingPlayer, InvestmentTile tile)
        {
            ActionPoint AP = tile.actionPoints.FirstOrDefault(ap => ap.actionType == ActionPoint.ActionType.Diplomacy);

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
