using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreDemandCommand : Command
{
    [SerializeField] Game.Resource resourceToScore;

    public override void Do(Action action)
    {
        // TODO: Present the Player with a Yes/No Choice Box

        GlobalDemandTrack track = Game.GlobalDemand;
        Dictionary<Game.ActionType, int> actions = Game.GlobalDemand[Phase.currentPhase.era][resourceToScore];

        Dictionary<Game.Faction, int> demandScore = new Dictionary<Game.Faction, int>() {
            { Game.Faction.Britain, GameObject.FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.Britain).Count() },
            { Game.Faction.France, GameObject.FindObjectsOfType<Market>().Where(market => market.flag == Game.Faction.France).Count() }
        };

        Game.Faction winningFaction = Game.Faction.Neutral;
        if (demandScore[Game.Faction.France] > demandScore[Game.Faction.Britain]) winningFaction = Game.Faction.France;
        else if (demandScore[Game.Faction.Britain] > demandScore[Game.Faction.France]) winningFaction = Game.Faction.Britain;

        // Todo: add the specific commands into the Global Demand Track
        //foreach (Game.ActionType action in actions.Keys) 
        //{ 
        //    switch (action)
        //    {
        //        // TODO Merge all these Adjust<Point>Commands into a single command that accepts a value parameter
        //        case Game.ActionType.VictoryPoint:
        //            AdjustVPCommand adjustVictoryPoints = gameObject.AddComponent<AdjustVPCommand>();
        //            adjustVictoryPoints.adjustAmount.value = actions[action]; 
        //            adjustVictoryPoints.targetFaction = winningFaction;
        //            adjustVictoryPoints.Do();
        //            break;
        //        case Game.ActionType.Treaty:

        //            AdjustTPCommand adjustTPCommand = gameObject.AddComponent<AdjustTPCommand>();
        //            adjustTPCommand.actingFaction = winningFaction;
        //            adjustTPCommand.adjustAmount.value = actions[action];
        //            adjustTPCommand.Do();
        //            break;
        //        case Game.ActionType.Debt:
        //            AdjustDebtCommand adjustDebt = gameObject.AddComponent<AdjustDebtCommand>();
        //            adjustDebt.targetFaction = winningFaction;
        //            adjustDebt.adjustAmt = actions[action];
        //            adjustDebt.Do();
        //            break;
        //    }
        //}
    }
}