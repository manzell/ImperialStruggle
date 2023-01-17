using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class ActsOfUnion_FR_Bonus : PlayerAction
    {
        [SerializeField] Map europe;
        [SerializeField] List<PoliticalData> excludedSpaces; 

        protected override async Task Do(IAction context)
        {
            IEnumerable<PoliticalSpace> eligibleSpaces = Game.Spaces.OfType<PoliticalSpace>()
                .Where(space => space.map == europe && !excludedSpaces.Contains(space.data) && space.Flag == Player.Opponent);

            if(eligibleSpaces.Count() > 0)
                await new Selection<PoliticalSpace>(Player, eligibleSpaces, Finish).Completion;
        }

        void Finish(Selection<PoliticalSpace> selection)
        {
            if(selection.Count() > 0)
                Commands.Push(new UnflagCommand(selection.First()));  
        }
    }
}
