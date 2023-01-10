using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImperialStruggle
{
    public class MinistryCard : ICard, ISelectable
    {
        public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
        public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }

        public MinistryCardStatus ministryCardStatus;
        public MinistryCardData data { get; private set; }

        public string Name => data.name;

        public MinistryCard(MinistryCardData data)
        {
            this.data = data;
        }

        public Action UISelectionEvent { get; set; }
        public Action UIDeselectEvent { get; set; }
    }
}