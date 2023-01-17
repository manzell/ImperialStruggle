using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class MilitarySpendingOverruns : PlayerAction
    {
        [SerializeField] List<PlayerAction> actions;

        protected override async Task Do(IAction context)
        {
            Selection<PlayerAction> selection = new(Player, actions);
            await selection.Completion;

            if (selection.Count() > 0)
                await selection.First().Execute(this); 
        }
    }
}
