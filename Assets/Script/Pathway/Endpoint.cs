using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    // Start is called before the first frame update
     void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        EventManager.TriggerEvent("Captured", collision.gameObject, null);
    }
}
