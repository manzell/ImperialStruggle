using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImperialStruggle;
using System;

namespace ImperialStruggle
{
    [CreateAssetMenu]
    public class EventCard : CardData, ICard, ISelectable
    {
        public Action UISelectionEvent { get; set; }
        public Action UIDeselectEvent { get; set; }

        public string Name => name;

        public ActionPoint.ActionType reqdActionType;

        public Dictionary<Faction, (GameAction baseAction, GameAction bonusAction)> cardActions;
        public Conditional bonusCondition; 
    }
}
