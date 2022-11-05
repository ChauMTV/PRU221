using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector]
    public EnemyNavigation enemyNav;
    [HideInInspector]
    public EnemyPath enemyPath;
    public EnemyState defaultState;
    public GameObject EndPoint;
    private List<EnemyState> enemyState = new List<EnemyState>();

    private EnemyState previousState;
    private EnemyState currentState;

    // Start is called before the first frame update
    void Awake()
    {
        if (enemyNav == null)
        {
            enemyNav = GetComponent<EnemyNavigation>();
        }
        if (enemyPath == null)
        {
            enemyPath = GetComponent<EnemyPath>();
        }
    }
    void Start()
    {
        // Get all AI states from this gameobject
        EnemyState[] states = GetComponents<EnemyState>();
        if (states.Length > 0)
        {
            foreach (EnemyState state in states)
            {
                // Add state to list
                enemyState.Add(state);
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



    public void ChangeState(EnemyState state)
    {
        if (state != null)
        {
            // Try to find such state in list
            foreach (EnemyState aiState in enemyState)
            {
                if (state == aiState)
                {
                    previousState = currentState;
                    currentState = aiState;
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
            Debug.Log("Go to default state " + enemyState[0]);
        }
    }
    public void GoToDefaultState()
    {
        previousState = currentState;
        currentState = defaultState;
        NotifyOnStateExit();
        DisableAllStates();
        EnableNewState();
        NotifyOnStateEnter();
    }

    void OnEnable()
    {
        // Enable AI on AiBehavior enabling
        if (currentState != null && currentState.enabled == false)
        {
            EnableNewState();
        }
    }

    /// <summary>
    /// Raises the disable event. chuyen cai nay vao enemy stats
    /// </summary>
    public void releaseMySelfOnDisable()
    {
        // Disable AI on AiBehavior disabling
        //DisableAllStates(); cua ngta

        //chuyen vo cai enemy stats thi bo comment
        //if(enemy_hp<=0)// cua thang dung~
        objectPool.Release(gameObject);
    }


    /// <summary>
    /// Turn off all AI states components.
    /// </summary>
    private void DisableAllStates()
    {
        foreach (EnemyState enState in enemyState)
        {
            enState.enabled = false;
        }
    }
    /// <summary>
    /// chuyen cai nay vai enemy stats
    /// </summary>
    ObjectPool<GameObject> objectPool;
    public void SetParentObjectPool(ObjectPool<GameObject> objectPool)
    {
        this.objectPool = objectPool;
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
    public void DetectEndPoint()
    {
        if (Vector3.Distance(transform.position, EndPoint.transform.position) <= 0.5f)
        {
            //original
            releaseMySelfOnDisable();
            enemyPath.path = null;
            enemyPath.destination = null;
        }

    }
    private void Update()
    {
        DetectEndPoint();
        Debug.Log("Navigation: " + enemyNav.destination);
        Debug.Log("destination :" + enemyPath.destination);
    }
    /// <summary>
    /// Raises the trigger event.
    /// </summary>
    /// <param name="trigger">Trigger.</param>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void OnTrigger(EnemyState.Trigger trigger, Collider2D my, Collider2D other)
    {
        if (currentState == null)
        {
            Debug.Log("Current sate is null");
        }
        currentState.OnTrigger(trigger, my, other);
    }
}
