using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImperialStruggle
{
    public class EventCard : CardData, ICard, ISelectable
    {
        public Action UISelectionEvent { get; set; }
        public Action UIDeselectEvent { get; set; }

        public string Name => name;

        public ActionPoint.ActionType reqdActionType;

        public Dictionary<Faction, (PlayerAction baseAction, PlayerAction bonusAction)> cardActions;
        public Conditional bonusCondition; 
    }
}
