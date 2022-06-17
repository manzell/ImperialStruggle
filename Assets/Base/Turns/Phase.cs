using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

public class Phase : SerializedMonoBehaviour
{
    public static Phase rootPhase;
    public static Phase currentPhase;
    public static UnityEvent<Phase>
        phaseStartEvent = new UnityEvent<Phase>(),
        phaseMidEvent = new UnityEvent<Phase>(),
        phaseEndEvent = new UnityEvent<Phase>();

    public Trigger phaseStartTrigger, phaseTrigger, phaseEndTrigger;

    public UnityAction callback;
    public Game.Era era;
    public List<BaseAction> executedActions = new List<BaseAction>();

    [SerializeField] List<BaseAction> phaseStartActions = new List<BaseAction>(),
        phaseEndActions = new List<BaseAction>();

    public Phase prevPhase;
    public Phase nextSibling => transform.parent.GetComponentsInChildren<Phase>()
        .Where(phase => phase != transform.parent.GetComponent<Phase>() && phase != this)
        .Where(phase => phase.transform.GetSiblingIndex() > transform.GetSiblingIndex() && phase.gameObject.activeInHierarchy).First();

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
    public Phase nextPhase => nextChild ? nextChild : nextSibling;


    [Button]
    public void StartThread()
    {
        Game.startGameEvent.Invoke(); 
        StartPhase(() => Game.Log("Thread Over"));
    }

    public virtual void StartPhase(UnityAction callback)
    {
        Debug.Log($"StartPhase:: {this}");
        this.callback = callback; 

        currentPhase = this;
        if (rootPhase == null) rootPhase = this; 

        phaseStartEvent.Invoke(this);
        phaseStartTrigger?.onTrigger.Invoke();
        RunActionSequence(phaseStartActions, () => OnPhase(callback));
    }

    void OnPhase(UnityAction callback)
    {
        phaseMidEvent.Invoke(this);
        phaseTrigger?.onTrigger.Invoke();
        AdvanceToChildPhase(callback);
    }

    // End Phase is called after OnPhase - it checks to see if we have any Child Phases to process
    // In which case it calls those with AfterPhase as the callback. Otherwise, it calls After Phase
    void AdvanceToChildPhase(UnityAction callback)
    {
        if (nextChild)
            nextChild.StartPhase(callback);
        else
            AfterPhase(callback); // this should point to AfterPhase(original callback)
    }

    // After phase is called either from the terminal phase of the Child Callback or the previous sibling
    void AfterPhase(UnityAction callback)
    {
        phaseEndEvent.Invoke(this);
        phaseEndTrigger?.onTrigger.Invoke();
        //Debug.Log($"AfterPhase::{this}");
        RunActionSequence(phaseEndActions, () => AdvanceToNextPhase(callback));
    }

    void AdvanceToNextPhase(UnityAction callback)
    {
        if (nextSibling)
            nextSibling.StartPhase(callback);
        else
            callback.Invoke(); 
    }

    public static void RunActionSequence(List<BaseAction> gameActions, UnityAction callback)
    {
        if (gameActions.Count > 0)
            TryAction(gameActions[0], callback);
        else
            callback.Invoke(); 

        void TryAction(BaseAction gameAction, UnityAction callback) =>
            gameAction.Try(() => CatchAction(gameAction, callback));

        void CatchAction(BaseAction gameAction, UnityAction callback)
        {
            int i = gameActions.IndexOf(gameAction) + 1;

            if (gameActions.Count > i)
                TryAction(gameActions[i], callback); 
            else
                callback.Invoke(); 
        }
    }
}