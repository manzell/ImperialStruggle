using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

public class Phase : SerializedMonoBehaviour
{
    public static Phase currentPhase;
    public static UnityEvent<Phase>
        phaseStartEvent = new UnityEvent<Phase>(),
        phaseMidEvent = new UnityEvent<Phase>(),
        phaseEndEvent = new UnityEvent<Phase>();

    public Game.Era era;

    [SerializeField] List<GameAction> phaseStartActions = new List<GameAction>(),
        phaseEndActions = new List<GameAction>(); 

    public Phase prevPhase;
    public Phase nextSibling
    {
        get
        {
            Phase[] siblings = transform.parent.GetComponents<Phase>()
                .Where(phase => phase.gameObject.activeInHierarchy == true && phase.transform.GetSiblingIndex() > transform.GetSiblingIndex())
                .ToArray();

            return siblings.Length > 0 ? siblings[0] : null;
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
    public Phase nextPhase => nextChild ? nextChild : nextSibling;


    [Button] public void StartThread() => StartPhase(() => Debug.Log("Thread Over"));
    public virtual void StartPhase(UnityAction callback)
    {
        currentPhase = this;

        phaseStartEvent.Invoke(this);
        SendPhaseActions(phaseStartActions, () => OnPhase(callback));
    }

    void OnPhase(UnityAction callback)
    {
        phaseMidEvent.Invoke(this);
        SendPhaseActions(phaseEndActions, () => AfterPhase(callback));
    }

    void AfterPhase(UnityAction callback)
    {
        if (nextChild)
            nextChild.StartPhase(callback);
        else 
            EndPhase(callback); 
    }

    void EndPhase(UnityAction callback)
    {
        phaseEndEvent.Invoke(this);

        if (nextSibling)
            nextSibling.StartPhase(callback);
        else
            callback.Invoke(); 
    }

    void SendPhaseActions(List<GameAction> gameActions, UnityAction callback)
    {
        if (gameActions.Count > 0)
            SendPhaseAction(gameActions[0], callback);
        else
            callback.Invoke(); 

        void SendPhaseAction(GameAction gameAction, UnityAction callback) => 
            gameAction.Try(() => ReceivePhaseAction(gameAction, callback)); 

        void ReceivePhaseAction(GameAction gameAction, UnityAction callback)
        {
            int i = gameActions.IndexOf(gameAction) + 1;

            if (gameActions.Count < i)
                SendPhaseAction(gameActions[i], callback); 
            else
                callback.Invoke(); 
        }
    }
}