using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPath : EnemyState
{

    [HideInInspector]
    public Pathway path;
    public bool loop = false;
    [HideInInspector]
    public Waypoint destination;


    public override void Awake()
    {
        base.Awake();
        //    Debug.Assert(enemyBehaviour.enemyNav, "Wrong initial parameters");
    }

    public override void OnStateEnter(EnemyState previousState, EnemyState newState)
    {
        if (path == null)
        {
            // If I have no path - try to find it
            path = FindObjectOfType<Pathway>();
            Debug.Assert(path, "Have no path");
        }
        if (destination == null)
        {
            // Get next waypoint from my path
            destination = path.GetNearestWaypoint(transform.position);
        }
        // Set destination for navigation agent
        enemyBehaviour.enemyNav.destination = destination.transform.position;
        // Start moving
        enemyBehaviour.enemyNav.move = true;
        enemyBehaviour.enemyNav.turn = true;
        // If unit has animator
        //if (anim != null && anim.runtimeAnimatorController != null)
        //{
        //    // Search for clip
        //    foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        //    {
        //        if (clip.name == "Move")
        //        {
        //            // Play animation
        //            anim.SetTrigger("move");
        //            break;
        //        }
        //    }
        //}
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void OnStateExit(EnemyState previousState, EnemyState newState)
    {
        // Stop moving
        enemyBehaviour.enemyNav.move = false;
        enemyBehaviour.enemyNav.turn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            // If I have no path - try to find it
            path = FindObjectOfType<Pathway>();
            Debug.Assert(path, "Have no path");
        }
        if (destination == null)
        {
            // Get next waypoint from my path
            destination = path.GetNearestWaypoint(transform.position);
        }
        // Set destination for navigation agent
        enemyBehaviour.enemyNav.destination = destination.transform.position;
        // Start moving
        enemyBehaviour.enemyNav.move = true;
        enemyBehaviour.enemyNav.turn = true;


        if (destination != null)
        {
            //if destination reached
            if((Vector2) destination.transform.position == (Vector2)transform.position)
            {
                destination = path.GetNextWaypoint(destination, loop);
                if (destination != null)
                {
                    enemyBehaviour.enemyNav.destination= destination.transform.position;
                }
            }
        }
    }
    public void UpdateDestination(bool getNearestWaypoint)
    {
        if (getNearestWaypoint == true)
        {
            // Get next waypoint from my path
            destination = path.GetNearestWaypoint(transform.position);
        }
        if (enabled == true)
        {
            
            // Set destination for navigation agent
            enemyBehaviour.enemyNav.destination = destination.transform.position;
        }
    }
    public float GetRemainingPath()
    {
        Vector2 distance = destination.transform.position - transform.position;
        return (distance.magnitude + path.GetPathDistance(destination));
    }
}
