using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class CalicoActs_BR_Bonus : PlayerAction
    {
        [SerializeField] Resource cotton; 
        protected override Task Do(IAction context)
        {
            Debug.LogWarning("Give the player the Boolean Yes/No option!"); 

            GlobalDemandKey demandKey = new(Phase.CurrentPhase.era, cotton);

            Faction resourceWinningFaction = null;

            int britainCount = Game.Spaces.OfType<Market>().Count(market => market.Control == Game.Britain && market.Resource == cotton);
            int franceCount = Game.Spaces.OfType<Market>().Count(market => market.Control == Game.France && market.Resource == cotton);

            if (britainCount > franceCount)
                resourceWinningFaction = Game.Britain;
            else if (franceCount > britainCount)
                resourceWinningFaction = Game.France;

            if(resourceWinningFaction != null)
                Commands.Push(new AddAPCommand(resourceWinningFaction.player, Game.GlobalDemandTrack.GlobalDemandAwards[demandKey])); 

            return Task.CompletedTask;
        }
    }

    public class CalicoActs_FR_Base : PlayerAction
    {
        [SerializeField] Resource cotton; 

        protected override async Task Do(IAction context)
        {
            HashSet<Market> cottonMarkets = new(Game.Spaces.OfType<Market>().Where(market => market.Resource == cotton));

            Selection<Market> select = new(Player, cottonMarkets);
            await select.Completion;

            if (select.Count() > 0)
                Commands.Push(new UnflagCommand(select.First()));
        }
    }

    public class CalicoActs_FR_Bonus : PlayerAction
    {
        protected override async Task Do(IAction context)
        {
            HashSet<Squadron> squadrons = new(Game.Britain.player.Squadrons.Where(squadron => squadron.space != null)); 

            Selection<Squadron> select = new(Player, squadrons);
            await select.Completion;

            if (select.Count() > 0)
                Commands.Push(new DeploySquadronCommand(select.First(), null)); 
        }
    }
}
