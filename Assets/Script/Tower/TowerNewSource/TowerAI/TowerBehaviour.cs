using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBehaviour : MonoBehaviour
{
    // Navigation agent if it is needed
    // This state will be activate on start
    [HideInInspector]
    public EnemyNavigation navAgent;
    public TowerState defaultState;

    // List with all states for this AI
    private List<TowerState> TowerStates = new List<TowerState>();
    // The state that was before
    private TowerState previousState;
    // Active state
    private TowerState currentState;


    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        // Enable AI on towerBehaviour enabling
        if (currentState != null && currentState.enabled == false)
        {
            EnableNewState();
        }
    }


    void Awake()
    {
        if (navAgent == null)
        {
            // Try to find navigation agent for this object
            navAgent = GetComponentInChildren<EnemyNavigation>();
        }
    }
    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        // Disable AI on towerBehaviour disabling
        DisableAllStates();
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        // Get all AI states from this gameobject
        TowerState[] states = GetComponents<TowerState>();
        if (states.Length > 0)
        {
            foreach (TowerState state in states)
            {
                // Add state to list
                TowerStates.Add(state);
            }
            if (defaultState != null)
            {
                // Set active and previous states as default state
                previousState = currentState = defaultState;
                if (currentState != null)
                {
                    // Go to active state
                    ChangeState(currentState);
                }
                else
                {
                    Debug.LogError("Incorrect default AI state " + defaultState);
                }
            }
            else
            {
                Debug.LogError("AI have no default state");
            }
        }
        else
        {
            Debug.LogError("No AI states found");
        }
    }

    /// <summary>
    /// Set AI to defalt state.
    /// </summary>
    public void GoToDefaultState()
    {
        previousState = currentState;
        currentState = defaultState;
        NotifyOnStateExit();
        DisableAllStates();
        EnableNewState();
        NotifyOnStateEnter();
    }

    /// <summary>
    /// Change Ai state.
    /// </summary>
    /// <param name="state">State.</param>
	public void ChangeState(TowerState state)
    {
        if (state != null)
        {
            // Try to find such state in list
            foreach (TowerState TowerState in TowerStates)
            {
                if (state == TowerState)
                {
                    previousState = currentState;
                    currentState = TowerState;
                    NotifyOnStateExit();
                    DisableAllStates();
                    EnableNewState();
                    NotifyOnStateEnter();
                    return;
                }
            }
            Debug.Log("No such state " + state);
            // If have no such state - go to default state
            GoToDefaultState();
            Debug.Log("Go to default state " + TowerStates[0]);
        }
    }

    /// <summary>
    /// Turn off all AI states components.
    /// </summary>
    private void DisableAllStates()
    {
        foreach (TowerState TowerState in TowerStates)
        {
            TowerState.enabled = false;
        }
    }

    /// <summary>
    /// Turn on active AI state component.
    /// </summary>
    private void EnableNewState()
    {
        currentState.enabled = true;
    }

    /// <summary>
    /// Send OnStateExit notification to previous state.
    /// </summary>
    private void NotifyOnStateExit()
    {
        previousState.OnStateExit(previousState, currentState);
    }

    /// <summary>
    /// Send OnStateEnter notification to new state.
    /// </summary>
    private void NotifyOnStateEnter()
    {
        currentState.OnStateEnter(previousState, currentState);
    }

    /// <summary>
    /// Raises the trigger event.
    /// </summary>
    /// <param name="trigger">Trigger.</param>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void OnTrigger(TowerState.Trigger trigger, Collider2D my, Collider2D other)
    {
        if (currentState == null)
        {
            Debug.Log("Current sate is null");
        }
        currentState.OnTrigger(trigger, my, other);
    }
}
