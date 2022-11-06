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

    public HealthBar healthBar;
    private HealthSystem healthSystem;
    public GameObject damagePoint;
    // Destination position
    [HideInInspector]
    public Vector2 destination;
    // Velocity vector
    [HideInInspector]
    public Vector2 velocity;
    [HideInInspector]
    public Animator anim;

    // Position on last frame
    private Vector2 prevPosition;

    void OnEnable()
    {
        prevPosition= transform.position;

    }
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        Debug.Log(healthSystem.GetHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
       
        // Calculate velocity
        velocity = (Vector2)transform.position - prevPosition;
        velocity /= Time.fixedDeltaTime;
        // If turning is allowed
        if (turn == true)
        {
            SetSpriteDirection(destination - (Vector2)transform.position);
        }
        prevPosition = transform.position;
    }

    /// <summary>
    /// Sets sprite direction on x axis.
    /// </summary>
    /// <param name="direction">Direction.</param>
    private void SetSpriteDirection(Vector2 direction)
    {
        // Flip gameobject dependings on direction
        if (direction.x > 0f && transform.localScale.x < 0f) // To the right
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0f && transform.localScale.x > 0f) // To the left
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // Set direction for 8d animation (if used coresponding animator)
        if (anim != null)
        {
            foreach (AnimatorControllerParameter param in anim.parameters)
            {
                if (param.name == "directionY")
                {
                    float directionY = direction.y / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
                    anim.SetFloat("directionY", directionY);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Looks at direction.
    /// </summary>
    /// <param name="direction">Direction.</param>
    public void LookAt(Vector2 direction)
    {
        SetSpriteDirection(direction);
    }

    /// <summary>
    /// Looks at target.
    /// </summary>
    /// <param name="target">Target.</param>
    public void LookAt(Transform target)
    {
        SetSpriteDirection(target.position - transform.position);
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        if (healthSystem.GetHealth() == 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
