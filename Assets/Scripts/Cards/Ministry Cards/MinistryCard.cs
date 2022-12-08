using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistryCard : MonoBehaviour, ICard, ISelectable
{
    public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
    public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }
    public MinistryCardStatus ministryCardStatus;
    public MinistryCardData ministryCardData;
    public bool exhausted; 

    public Faction faction => ministryCardData.faction;
    public List<Keyword> keywords => ministryCardData.keywords;
    public List<Phase.Era> eras => ministryCardData.eras;
    public string cardText => ministryCardData.cardText;

}