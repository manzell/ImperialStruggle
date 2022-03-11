using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistryCard : MonoBehaviour, ICard
{
    public Game.Faction faction;
    public List<Game.Keyword> keywords = new List<Game.Keyword>();
    public List<Game.Era> eras;
    public string cardText; 

    public bool exhausted, revealed;
}