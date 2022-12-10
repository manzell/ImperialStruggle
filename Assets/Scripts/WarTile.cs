using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ImperialStruggle
{
    public class WarTile : MonoBehaviour, ISelectable
    {
        public string Name => $"{warTileSet}: {value} {(debt ? "D " : "")}{(milDamage ? "M " : "")}{(unflag ? "U " : "")}";

        public string subname;
        public enum WarTileSet { Basic, Bonus }
        public int value;
        public WarTileSet warTileSet;
        public List<GameAction> actions;
        public List<Command> commands;
        public bool debt, milDamage, unflag;
        public Faction faction;
    }
}