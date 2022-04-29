using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistryCard : MonoBehaviour, ICard
{
    public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }
    public Game.Faction faction;
    public List<Game.Keyword> keywords = new List<Game.Keyword>();
    public List<Game.Era> eras;
    public MinistryCardStatus ministryCardStatus;
    public string cardText;
}