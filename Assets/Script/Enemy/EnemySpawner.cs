using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject mobprefab;
    [SerializeField]
    GameObject spawnerPos;
    //[SerializeField]
    //public float mobSpe
    // Start is called before the first frame update
    private float speed = 2;
    void Start()
    {
        StartCoroutine(spawnEnemy(mobprefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(speed);

        Instantiate(enemy, spawnerPos.transform.position, Quaternion.identity);
        StartCoroutine(spawnEnemy(enemy));
    }
}
