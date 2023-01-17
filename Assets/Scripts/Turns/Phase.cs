using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

namespace ImperialStruggle
{
    public abstract class Phase : SerializedMonoBehaviour
    {
        public enum Era { Succession, Empire, Revolution }

        public static Phase rootPhase;
        public static Phase PreviousPhase, CurrentPhase;
        public static System.Action<Phase> PhaseStartEvent, PhaseEndEvent;

        [field: SerializeField] public Era era { get; private set; }
        [SerializeField] protected Queue<IAction> PhaseStartActions, PhaseEndActions;
        [HideInInspector] public System.Action StartEvent, EndEvent;

        public Stack<Command> ExecutedCommands { get; private set; }


        Phase nextChild => GetComponentsInChildren<Phase>().Where(child => child != this).FirstOrDefault();
        Phase nextSibling => transform.parent.GetComponentsInChildren<Phase>().Where(actionRound => actionRound.transform.GetSiblingIndex() == transform.GetSiblingIndex() + 1).FirstOrDefault();
        Phase nextParent => transform.GetComponentInParent<Phase>().FollowingPhase;

        public virtual Phase NextPhase => nextChild ?? nextSibling ?? nextParent;
        public virtual Phase FollowingPhase => nextSibling ?? nextParent;

        private void Awake()
        {
            ExecutedCommands = new();
        }

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
            ProcessActionQueue(PhaseStartActions); 
        }

        public void Advance()
        {
            if (nextChild != null)
                nextChild.StartPhase();
            else
                EndPhase();
        }

        public virtual void EndPhase()
        {
            ProcessActionQueue(PhaseEndActions);
            EndEvent?.Invoke();
            PhaseEndEvent?.Invoke(this);

            FollowingPhase.StartPhase();
        }

        public void Push(IAction action)
        {
            foreach (Command command in action.Commands)
            {
                command.Do(action);
                ExecutedCommands.Push(command);
            }
        } 

        public void Push(Command command)
        {
            command.Do(null);
            ExecutedCommands.Push(command);
        }

        protected async void ProcessActionQueue(Queue<IAction> queue)
        {
            Phase phase = CurrentPhase;

            while (phase == CurrentPhase && queue.Count() > 0)
            {
                IAction next = queue.Dequeue();
                await next.Execute();
            }
        }

        public abstract bool Completed { get; }
    }
}