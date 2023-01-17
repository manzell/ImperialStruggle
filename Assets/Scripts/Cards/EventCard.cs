using System.Collections;
using System.Collections.Generic;

namespace ImperialStruggle
{
    public class EventCard : CardData, ICard, ISelectable
    {
        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public string Name => name;

        public ActionPoint.ActionType reqdActionType;

        public Dictionary<Faction, (PlayerAction baseAction, PlayerAction bonusAction)> cardActions;
        public Conditional<PlayerAction> bonusCondition; 
    }
}
