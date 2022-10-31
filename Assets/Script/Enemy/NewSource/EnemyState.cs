using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum Trigger
    {
        TriggerEnter,   // On collider enter
        TriggerStay,    // On collider stay
        TriggerExit,    // On collider exit
        Damage,         // On damage taken
        Cooldown,       // On some cooldown expired
        Alone			// When no other colliders intersect during time
    }

    [Serializable]

    public class EnemyTransact
    {
        public Trigger trigger;
        public EnemyState newState;
    }

    public EnemyTransact[] specificTransact;

    protected EnemyBehaviour enemyBehaviour;
    // Start is called before the first frame update

    public virtual void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    public virtual void OnStateEnter(EnemyState previousState, EnemyState newState)
    {

    }
    public virtual void OnStateExit(EnemyState previousState, EnemyState newState)
    {

    }
    public virtual bool OnTrigger (Trigger trig, Collider2D my, Collider2D other)
    {
        bool res = false;
        foreach(EnemyTransact transact in specificTransact)
        {
            if(trig == transact.trigger)
            {
                enemyBehaviour.ChangeState(transact.newState);
                res = true;
                break;
            }
        }
        return res;
    }
}
