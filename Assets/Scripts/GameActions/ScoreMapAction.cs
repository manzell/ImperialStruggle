using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ImperialStruggle
{
    public class ScoreMapAction : GameAction
    {
        public static System.Action<Map, AwardTile> scoreMapEvent;

        protected override Task Do()
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                foreach (KeyValuePair<Map, AwardTile> award in peaceTurn.awardTiles)
                {
                    scoreMapEvent?.Invoke(award.Key, award.Value);

                    if (award.Key.mapScore.Max(kvp => kvp.Value) - award.Key.mapScore.Min(kvp => kvp.Value) >= award.Value.RequiredMargin)
                        Commands.Push(new AddActionPointCommand(award.Key.controllingFaction.player, award.Value.ActionPoints));
                }
            }

            return Task.CompletedTask; 
        }
    }
}