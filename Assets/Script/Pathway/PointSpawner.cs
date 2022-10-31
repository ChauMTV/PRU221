using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PointSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public float delayedBeforeWave;
        public List<GameObject> enemies = new List<GameObject>();
    }

    public float RandomSpeed = 0.4f;
    public float unitSpawnDelay = 4f;
    public List<Wave> waves;
    public bool endlesswave = false;
    [HideInInspector]
    public List<GameObject> randomEnemiesList = new List<GameObject>();

    private Pathway path;
    private float counter;
    //Buffer active spawned enemies
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool finished = false;


    // Start is called before the first frame update

    void Awake()
    {
        path = GetComponentInParent<Pathway>();
        Debug.Assert(path != null, "Wrong initial parameters");
    }
    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("UnitDie", UnitDie);
        EventManager.StartListening("WaveStart", WaveStart);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>

    void OnDisable()
    {
        EventManager.StopListening("UnitDie", UnitDie);
        EventManager.StopListening("WaveStart", WaveStart);
    }

    void Update()
    {

    }




    private IEnumerator RunWave(int waveIdx)
    {

        if (waves.Count > waveIdx)
        {
            yield return new WaitForSeconds(waves[waveIdx].delayedBeforeWave);

            foreach (GameObject enemy in waves[waveIdx].enemies)
            {
                GameObject prefab = null;
                prefab = enemy;
                // If enemy prefab not specified - spawn random enemy
                if (prefab == null && randomEnemiesList.Count > 0)
                {
                    prefab = randomEnemiesList[Random.Range(0, randomEnemiesList.Count)];
                }
                if (prefab == null)
                {
                    Debug.LogError("Have no enemy prefab. Please specify enemies in Level Manager or in Spawn Point");
                }
                // Create enemy
                GameObject newEnemy = Instantiate(prefab, transform.position, transform.rotation);
                newEnemy.name = prefab.name;
                // Set pathway
                newEnemy.GetComponent<EnemyPath>().path = path;
                EnemyNavigation enemyNav = newEnemy.GetComponent<EnemyNavigation>();
                enemyNav.speed = Random.Range(enemyNav.speed * (1f - RandomSpeed), enemyNav.speed * (1f + RandomSpeed));

                // Add enemy to list
                activeEnemies.Add(newEnemy);
                // Wait for delay before next enemy run
                yield return new WaitForSeconds(unitSpawnDelay);
            }
            if (waveIdx + 1 == waves.Count)
            {
                finished = true;
                endlesswave = true;

            }
        }
        if (endlesswave == true)
        {
            while (endlesswave == true)
            {
                Debug.Log("spawned");
                GameObject prefab = randomEnemiesList[Random.Range(0, randomEnemiesList.Count)];
                GameObject newEnemy = Instantiate(prefab, transform.position, transform.rotation);
                newEnemy.name = prefab.name;
                newEnemy.GetComponent<EnemyPath>().path = path;
                EnemyNavigation enemyNav = newEnemy.GetComponent<EnemyNavigation>();
                enemyNav.speed = Random.Range(enemyNav.speed * (1f - RandomSpeed), enemyNav.speed * (1f + RandomSpeed));
                activeEnemies.Add(newEnemy);

                yield return new WaitForSeconds(unitSpawnDelay);
            }
        }
    }

    private void UnitDie(GameObject obj, string param)
    {
        // If this is active enemy
        if (activeEnemies.Contains(obj) == true)
        {
            // Remove it from buffer
            activeEnemies.Remove(obj);
        }
    }

    // Wave start event received
    private void WaveStart(GameObject obj, string param)
    {
        int waveIdx;
        int.TryParse(param, out waveIdx);
        StartCoroutine("RunWave", waveIdx);
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        StopAllCoroutines();
    }


}