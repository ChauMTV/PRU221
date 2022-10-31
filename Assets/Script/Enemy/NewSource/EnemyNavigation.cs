using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{

    public float speed = 1f;
    [HideInInspector]
    public bool move = true;
    // Can turning
    [HideInInspector]
    public bool turn = true;
    // Destination position
    [HideInInspector]
    public Vector2 destination;
    // Velocity vector
    [HideInInspector]
    public Vector2 velocity;

    // Position on last frame
    private Vector2 prevPosition;

    void OnEnable()
    {
        prevPosition= transform.position;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            Debug.Log("my speed " + speed);
        }
       
        // Calculate velocity
        velocity = (Vector2)transform.position - prevPosition;
        velocity /= Time.fixedDeltaTime;
        // If turning is allowed
         prevPosition = transform.position;
    }
}
