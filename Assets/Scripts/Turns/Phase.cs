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
        public System.Action StartEvent, EndEvent;

        [SerializeField] Queue<GameAction> phaseStartActions = new(), phaseEndActions = new();
        Stack<GameAction> executedActions = new();

        [field: SerializeField] public Era era { get; private set; }

        Phase childPhase => GetComponentsInChildren<Phase>().FirstOrDefault();
        Phase siblingPhase => transform.parent.GetComponentsInChildren<Phase>().Where(actionRound => actionRound.transform.GetSiblingIndex() == transform.GetSiblingIndex() + 1).FirstOrDefault();
        Phase parentPhase => transform.GetComponentInParent<Phase>().FollowingPhase;

        public virtual Phase NextPhase => childPhase ?? siblingPhase ?? parentPhase;
        public virtual Phase FollowingPhase => siblingPhase ?? parentPhase;

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

            if (Completed)
                NextPhase.StartPhase();
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
                command.Do(action); 
        } 

        void ProcessActionQueue(Queue<GameAction> queue)
        {
            // TODO: Look into an Iterator. If one of the Actions triggers the end game state we dont want to keep going through this list. 
            //       Note: Checking that CurrentPhase is consistent help, but isn't a real solution. 
            Phase phase = CurrentPhase;

            while (phase == CurrentPhase && queue.Count > 0)
                queue.Dequeue().Execute();
        }

        public abstract bool Completed { get; }
    }
}