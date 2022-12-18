using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistryCard : ICard, ISelectable
{
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
    public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }

    public MinistryCardStatus ministryCardStatus;
    public ImperialStruggle.MinistryCardData data { get; private set; }

    public string Name => data.name;

    public Action UISelectionEvent { get; set; }
    public Action UIDeselectEvent { get ; set; }
}