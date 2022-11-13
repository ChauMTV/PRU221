using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerState : MonoBehaviour
{// Allowed triiger types for AI state transactions
    public enum Trigger
    {
        TriggerEnter,   // On collider enter
        TriggerStay,    // On collider stay
        TriggerExit,    // On collider exit
        Damage,         // On damage taken
        Cooldown,       // On some cooldown expired
        Alone           // When no other colliders intersect during time
    }

    [Serializable]
    // Allows to specify AI state change on any trigger
    public class AiTransaction
    {
        public Trigger trigger;
        public TowerState newState;
    }
    // List with specified transactions for this AI state
    public AiTransaction[] specificTransactions;

    // Animation controller for this AI
    protected Animator anim;
    // AI behavior of this object
    protected TowerBehaviour towerBehaviour;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    public virtual void Awake()
    {   
        towerBehaviour = GetComponent<TowerBehaviour>();
        anim = GetComponentInParent<Animator>();
        Debug.Assert(towerBehaviour, "Wrong initial parameters");
    }

    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public virtual void OnStateEnter(TowerState previousState, TowerState newState)
    {

    }

    /// <summary>
    /// Raises the state exit event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public virtual void OnStateExit(TowerState previousState, TowerState newState)
    {

    }

    /// <summary>
    /// Raises the trigger event.
    /// </summary>
    /// <param name="trigger">Trigger.</param>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public virtual bool OnTrigger(Trigger trigger, Collider2D my, Collider2D other)
    {
        bool res = false;
        // Check if this AI state has specific transactions for this trigger
        foreach (AiTransaction transaction in specificTransactions)
        {
            if (trigger == transaction.trigger)
            {
                towerBehaviour.ChangeState(transaction.newState);
                res = true;
                break;
            }
        }
        return res;
    }
}
