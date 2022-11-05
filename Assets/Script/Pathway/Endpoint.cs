using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Endpoint : MonoBehaviour
{
    ObjectPool<GameObject> pool;
    public void SetParentObjectPool(ObjectPool<GameObject> objectPool)
    {
        this.pool = objectPool;
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            EventManager.TriggerEvent("Captured", collision.gameObject, null);
        }
        //collision.gameObject.GetComponent<EnemyBehaviour>().releaseMySelf();

    }
}
