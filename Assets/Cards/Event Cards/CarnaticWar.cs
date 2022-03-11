using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq; 

public class CarnaticWar : EventCard
{
    [SerializeField] Game.Keyword bonusKeyword; 
    [SerializeField] List<Space> indianAlliances, fortsAndMarkets;

    public override void Play(ActionRound actionRound)
    {
        if (commandText.ContainsKey(actionRound.actingFaction))
            Debug.Log(commandText[actionRound.actingFaction]);
        else
            Debug.Log(commandText[Game.Faction.Neutral]); 
    }

    [Button(Name ="Place Conflict Markers")]
    public void PlaceConflictMarkers(Game.Faction faction, List<Space> targetIndianSpaces)
    {
        int conflictMarkers = indianAlliances
            .Where(space => space.flag == faction && space.conflictMarker == false)
            .Count(); 

        targetIndianSpaces.Take(conflictMarkers).ToList().ForEach(space => space.conflictMarker = true);

        // Create Place Conflict Marker Game Action and put it in the Phase List
        PlaceConflictMarker baseEffect = new PlaceConflictMarker(targetIndianSpaces);
        baseEffect.Do(faction); 
        (Phase.currentPhase as ActionRound).gameActions.Add(baseEffect);

        bool bonusAction = false; 

        foreach(MinistryCard minister in Player.players[faction].ministers)
            if (minister.keywords.Contains(bonusKeyword))
                bonusAction = true;

        if (!bonusAction)
            callback.Invoke(); 
    }

    [Button(Name ="BONUS: Damage Fort or Shift Cotton Market")]
    public void SelectBonusSpace(Game.Faction faction, Space space)
    {
        //Test
        if(space is Market)
            ShiftCottonMarket(faction, space as Market);
        else if (space is MilSpace)
            DamageFort(faction, space as MilSpace);

        callback.Invoke(); 
    }

    public void DamageFort(Game.Faction faction, MilSpace fort)
    {
        // Create Damage Enemy Fort Game Action
        if (fort.spaceType == MilSpace.SpaceType.Fort && fort.flag != faction && fort.flag != Game.Faction.Neutral)
            (Phase.currentPhase as ActionRound).gameActions.Add(new DamageFort(faction, fort));
    }

    public void ShiftCottonMarket(Game.Faction faction, Market market)
    {
        // Create a Shift Market Game Action
        if (market.marketType == Game.Resource.Cotton && market.flag != faction)
            (Phase.currentPhase as ActionRound).gameActions.Add(new ShiftSpace(market, faction));
    }
}