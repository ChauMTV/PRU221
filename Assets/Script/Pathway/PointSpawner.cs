using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Pool;
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
    public static int enemyIndex;
    private Pathway path;
    public ObjectPool<GameObject> _pool;
    public float endlessWaveStartTimer = 6f;
    private GameObject newEnemy;
    private float counter;
    //Buffer active spawned enemies
    public static List<GameObject> activeEnemies = new List<GameObject>();
    public static List<EnemyNavigation> enemyBehaviours = new List<EnemyNavigation>();
    private bool finished = false;

    public int maxPoolSize = 10;

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

    public static GameObject GetClosestEnemy(Vector3 position, float maxRange)
    {
        GameObject closestEnemy = null;
        foreach (GameObject enemy in activeEnemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) <= maxRange)
            {
                if (closestEnemy == null)
                {
                    closestEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.transform.position) < Vector3.Distance(position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemy;
                    }
                }
            }
        }
        return closestEnemy;
    }
    private IEnumerator RunWave(int waveIdx)
    {

        if (waves.Count > waveIdx)
        {
            yield return new WaitForSeconds(waves[waveIdx].delayedBeforeWave);

            foreach (GameObject enemy in waves[waveIdx].enemies)
            {

                enemyIndex = Random.Range(0, randomEnemiesList.Count);
                GameObject prefab = null;
                prefab = enemy;
                // If enemy prefab not specified - spawn random enemy
                if (prefab == null && randomEnemiesList.Count > 0)
                {
                    prefab = randomEnemiesList[enemyIndex];
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
                enemyBehaviours.Add(enemyNav);
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
            _pool = new ObjectPool<GameObject>(() => { return Instantiate(randomEnemiesList[Random.Range(0, randomEnemiesList.Count)]); }, enemy =>
            {
                enemy.SetActive(true);

            }, enemy =>
            {
                enemy.SetActive(false);
                enemy.GetComponent<EnemyPath>().path = null;
                enemy.GetComponent<EnemyPath>().destination = null;
                activeEnemies.Remove(enemy);
            }, enemy =>
            {
                Destroy(enemy);
            }, false, 5, maxPoolSize
  );
            while (endlesswave == true)
            {
                endlessWaveStartTimer -= Time.deltaTime;
                //if (endlessWaveStartTimer <= 0)
                //{
                //    Debug.Log(endlessWaveStartTimer);
                //    enemyIndex = Random.Range(0, randomEnemiesList.Count);
                //    GameObject prefab = randomEnemiesList[enemyIndex];
                //    if (_pool.CountActive < maxPoolSize)
                //    {
                //        newEnemy = _pool.Get();

                //        //newEnemy.GetComponent<EnemyBehaviour>().SetParentObjectPool(_pool);
                //        newEnemy.transform.position = transform.position;
                //        Debug.Log("active: " + _pool.CountActive);
                //        Debug.Log("dang de");

                //        newEnemy.GetComponent<EnemyPath>().path = path;
                //        ///////////////////////////////////////////////////////////////
                //        //newEnemy.GetComponent<EnemyPath>().destination = null;
                //        EnemyNavigation enemyNav = newEnemy.GetComponent<EnemyNavigation>();
                //        ////////////////////////////////////////////////////////////////
                //        enemyNav.enemyIndex = enemyIndex;
                //        enemyNav.speed = Random.Range(enemyNav.speed * (1f - RandomSpeed), enemyNav.speed * (1f + RandomSpeed));
                //        activeEnemies.Add(newEnemy);
                //        enemyBehaviours.Add(enemyNav);
                //    }
                enemyIndex = Random.Range(0, randomEnemiesList.Count);
                GameObject prefab = randomEnemiesList[enemyIndex];
                GameObject newEnemy = Instantiate(prefab, transform.position, transform.rotation);
                newEnemy.name = prefab.name;
                newEnemy.GetComponent<EnemyPath>().path = path;
                EnemyNavigation enemyNav = newEnemy.GetComponent<EnemyNavigation>();
                enemyNav.enemyIndex = enemyIndex;
                enemyNav.speed = Random.Range(enemyNav.speed * (1f - RandomSpeed), enemyNav.speed * (1f + RandomSpeed));
                activeEnemies.Add(newEnemy);
                enemyBehaviours.Add(enemyNav);

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
