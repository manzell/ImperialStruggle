using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class SelectionAction : PlayerAction
    {
        [SerializeField] string prompt;
        [SerializeField] Calculation<IEnumerable<ISelectable>> source;
        [SerializeField] SelectionReceiver<ISelectable> response;
        [SerializeField] int min = 1, max = 1; 

        protected override async Task Do(IAction context)
        {
            Selection<ISelectable> selection = new(Player, source.Calculate(this), response.OnSelect);
            selection.SetTitle(prompt);
            selection.SetMinMax(min, max); 
            await selection.Completion; 
        }
    }

    public abstract class SelectionReceiver<T> where T : ISelectable
    {
        public string Name { get; protected set; }
        public IAction UISelectionEvent { get; set; }
        public IAction UIDeselectEvent { get; set; }

        protected Stack<Command> Commands = new Stack<Command>();

        public abstract void OnSelect(Selection<T> selection);
    }
}
