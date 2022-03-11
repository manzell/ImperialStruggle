using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq; 

public class Phase : SerializedMonoBehaviour
{
    public Game.Era era; 
    public static UnityEvent<Phase>
        phaseStartEvent = new UnityEvent<Phase>(),
        phaseEndEvent = new UnityEvent<Phase>();

    public List<IPhaseAction>
        beforePhaseActions = new List<IPhaseAction>(),
        onPhaseActions = new List<IPhaseAction>(),
        afterPhaseActions = new List<IPhaseAction>();

    [HideInInspector] public List<GameAction> gameActions; 
    [HideInInspector] public UnityAction callback;

    public static Phase currentPhase;
    
    public Phase prevPhase; 
    public Phase nextSibling
    {
        get
        {
            List<Phase> siblings = transform.parent.GetComponents<Phase>()
                .Where(phase => phase.gameObject.activeInHierarchy == true && phase.transform.GetSiblingIndex() > transform.GetSiblingIndex())
                .ToList();

            return siblings.Count > 0 ? siblings[0] : null;
        }
    }
    public Phase nextChild
    {
        get
        {
            List<Phase> children = GetComponentsInChildren<Phase>().Where(phase => phase != this && phase.gameObject.activeInHierarchy == true).ToList();
            return children.Count > 0 ? children[0] : null; 
        }
    }
    public Phase parentPhase
    {
        get
        {
            List<Phase> parents = transform.GetComponentsInParent<Phase>().Where(phase => phase != this && phase.gameObject.activeInHierarchy == true).ToList();
            return parents.Count > 0 ? parents[0] : null;
        }
    }

    [Button]
    public void StartThread() => StartPhase(() => Debug.Log("Thread Over"));
    public virtual void StartPhase(UnityAction callback)
    {
        this.callback = callback;
        currentPhase = this;

        phaseStartEvent.Invoke(this);

        ProcessPhaseActions(beforePhaseActions, () =>
            ProcessPhaseActions(onPhaseActions, () => 
                OnPhase(callback)));
    }

    public virtual void OnPhase(UnityAction callback) => NextPhase(callback);

    public void NextPhase(UnityAction callback)
    {
        if (nextChild)
        {
            nextChild.prevPhase = this; 
            nextChild.StartPhase(() => EndPhase(callback));
        }
        else
        {
            EndPhase(callback);
        }
    }

    public virtual void EndPhase(UnityAction callback)
    {
        phaseEndEvent.Invoke(this);

        ProcessPhaseActions(afterPhaseActions, () => Finalize(callback));
    }

    void Finalize(UnityAction callback)
    {
        if (nextSibling)
        {
            nextSibling.prevPhase = this;
            nextSibling.StartPhase(callback);
        }
        else if (parentPhase)
        {
            parentPhase.EndPhase(callback);
        }
        else
        {
            callback.Invoke(); // this is Thread Over if it happens. 
        }
    }

    void ProcessPhaseActions(List<IPhaseAction> onPhaseActions, UnityAction callback)
    {
        if (onPhaseActions.Count > 0)
        {
            IPhaseAction onPhaseAction = onPhaseActions[0];
            onPhaseActions.Remove(onPhaseAction);
            onPhaseAction.Do(this, () => ProcessPhaseActions(onPhaseActions, callback));
        }
        else
        {
            callback.Invoke();
        }
    }
}

public interface IPhaseAction
{
    public void Do(Phase phase, UnityAction callback);
}