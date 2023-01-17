using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class AlberonisAmbition : MonoBehaviour
    {
        public class AdjacentToBritishMarketCondition : Conditional<Space>
        {
            protected override bool Test(Space space) => space.adjacentSpaces.OfType<Market>().Any(neighbor => neighbor.Flag == Game.Britain);
        }

        public class Alberonis_FR_Base : PlayerAction
        {
            [SerializeField] HashSet<PoliticalData> eligibleSpaces;

            protected override async Task Do(IAction context)
            {
                await new Selection<PoliticalSpace>(Player, eligibleSpaces.Select(space => Game.SpaceLookup[space] as PoliticalSpace), Finish).Completion; 
            }

            void Finish(Selection<PoliticalSpace> selection)
            {
                if(selection.Count() > 0)
                    Commands.Push(new ShiftSpaceCommand(selection.First(), Game.France)); 
            }
        }

        public class Alberonis_FR_Bonus : PlayerAction
        {
            [SerializeField] HashSet<PoliticalData> spaces;

            protected override Task Do(IAction context)
            {
                if (spaces.All(space => Game.SpaceLookup[space].Flag == Player.Faction))
                    Commands.Push(new AdjustVPCommand(Player.Faction, 3));

                return Task.CompletedTask; 
            }
        }
    }
}
