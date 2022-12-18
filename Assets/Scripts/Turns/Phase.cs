using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;
using Sirenix.OdinInspector.Editor.Drawers;
using System;

namespace ImperialStruggle
{
    public abstract class Phase : SerializedMonoBehaviour
    {
        public enum Era { Succession, Empire, Revolution }

        public static Phase rootPhase;
        public static Phase PreviousPhase, CurrentPhase;
        public static System.Action<Phase> PhaseStartEvent, PhaseEndEvent;
        public System.Action StartEvent, EndEvent;

        [SerializeField] protected Queue<GameAction> phaseStartActions, phaseEndActions;
        public Stack<GameAction> ExecutedActions { get; private set; }
        public Stack<Command> ExecutedCommands { get; private set; }

        [field: SerializeField] public Era era { get; private set; }

        Phase childPhase => GetComponentsInChildren<Phase>().Where(child => child != this).FirstOrDefault();
        Phase siblingPhase => transform.parent.GetComponentsInChildren<Phase>().Where(actionRound => actionRound.transform.GetSiblingIndex() == transform.GetSiblingIndex() + 1).FirstOrDefault();
        Phase parentPhase => transform.GetComponentInParent<Phase>().FollowingPhase;

        public virtual Phase NextPhase => childPhase ?? siblingPhase ?? parentPhase;
        public virtual Phase FollowingPhase => siblingPhase ?? parentPhase;

        private void Awake()
        {
            ExecutedActions = new(); 
            ExecutedCommands = new();
        }

        [ContextMenu("Start Phase")]
        public virtual void StartPhase()
        {
            Debug.Log($"StartPhase:: {this}");

            if (rootPhase == null)
                rootPhase = this;
            else
                PreviousPhase = CurrentPhase;

            CurrentPhase = this;

            StartEvent?.Invoke();
            PhaseStartEvent?.Invoke(this);
            ProcessActionQueue(phaseStartActions);
        }

        public void Advance()
        {
            if (childPhase != null)
                childPhase.StartPhase();
            else
                EndPhase();
        }

        public virtual void EndPhase()
        {
            ProcessActionQueue(phaseEndActions);
            EndEvent?.Invoke();
            PhaseEndEvent?.Invoke(this);

            FollowingPhase.StartPhase();
        }

        public void Push(GameAction action)
        {
            foreach (Command command in action.Commands)
            {
                command.Do(null);
                ExecutedCommands.Push(command);
            }
        } 

        public void Push(Command command)
        {
            command.Do(null);
            ExecutedCommands.Push(command);
        }

        async void ProcessActionQueue(Queue<GameAction> queue)
        {
            // TODO: Look into an Iterator. If one of the Actions triggers the end game state we dont want to keep going through this list. 
            //       Note: Checking that CurrentPhase is consistent help, but isn't a real solution. 
            Phase phase = CurrentPhase;

            while (phase == CurrentPhase && queue.Count > 0)
            {
                GameAction action = queue.Dequeue(); 
                await action.Execute();

                ExecutedActions.Push(action); 
            }
        }

        public abstract bool Completed { get; }
    }
}