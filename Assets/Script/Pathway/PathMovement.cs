using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{

    private Transform target;

    private int wavepointIndex = 0;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoint.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
 
        if (Vector2.Distance(transform.position, target.position) <= 0.0065f)

        //You may be wondering why is it 0.0065f?
        // because that is how close it needs to be before it goes to another one
        // to much though can make the gameobject try to get the exact spot
        // and kind of looks like its glitching out
        {

            GetNextWaypoint();

        }
    }
    void GetNextWaypoint()
    {

        if (wavepointIndex >= Waypoint.points.Length - 1)
        {

            End();

            return;
        }
        wavepointIndex++;
        target = Waypoint.points[wavepointIndex];
    }
    void End()
    {

        Destroy(gameObject);
        Debug.Log("i died");
    }


}
