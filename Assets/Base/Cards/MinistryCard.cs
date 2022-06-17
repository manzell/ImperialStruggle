using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistryCard : MonoBehaviour, ICard, ISelectable
{
    public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }
    public MinistryCardStatus ministryCardStatus;
    public MinistryCardData ministryCardData;
    public bool exhausted; 

    public Game.Faction faction => ministryCardData.faction;
    public List<Game.Keyword> keywords => ministryCardData.keywords;
    public List<Game.Era> eras => ministryCardData.eras;
    public string cardText => ministryCardData.cardText;

}