using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public int gold = 50;

    public int CapturedPoint = 10;

    public List<GameObject> enemyList = new List<GameObject>();

    public List<GameObject> towerList = new List<GameObject>();

    // User interface manager
    private UiManager uiManager;
    // Nymbers of enemy spawners in this level
    private int spawnNumbers;
    // Current loose counter
    private int beforeLooseCounter;
    // Victory or defeat condition already triggered
    private bool triggered = false;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        PointSpawner[] spawners = FindObjectsOfType<PointSpawner>();
        spawnNumbers = spawners.Length;
        Debug.Log(enemyList.Count);
        if (spawnNumbers < 0)
        {
            Debug.LogError("No spawns");
        }
        foreach(PointSpawner spawner in spawners)
        {
            spawner.randomEnemiesList = enemyList;
        }
        uiManager.SetGold(gold);
        beforeLooseCounter = CapturedPoint;
        uiManager.SetDefeatAttempts(beforeLooseCounter);
    }


    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("Captured", Captured);
        EventManager.StartListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("Captured", Captured);
        EventManager.StopListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// Enemy reached capture point.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void Captured(GameObject obj, string param)
    {
        if (beforeLooseCounter > 0)
        {
            beforeLooseCounter--;
            uiManager.SetDefeatAttempts(beforeLooseCounter);
            if (beforeLooseCounter <= 0)
            {
                triggered = true;
                // Defeat
                EventManager.TriggerEvent("Defeat", null, null);
            }
        }
    }

    /// <summary>
    /// All enemies are dead.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void AllEnemiesAreDead(GameObject obj, string param)
    {
        spawnNumbers--;
        // Enemies dead at all spawners
        if (spawnNumbers <= 0)
        {
            // Check if loose condition was not triggered before
            if (triggered == false)
            {
                // Victory
                EventManager.TriggerEvent("Victory", null, null);
            }
        }
    }
}
