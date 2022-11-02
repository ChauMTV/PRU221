using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Tower : MonoBehaviour
{
    public int towerType;
    private Vector3 projectileShooting;
    private float range;
    private float shootTimerMax;
    private float shootTimer;
    [SerializeField]
    TowerWheel towerWheel;
    ProjecTileShooting projecTileShooting;
    private void Start()
    {
        towerType = GameObject.Find("TowerWheel").GetComponent<TowerWheel>().preTowerType;
    }
    private void Awake()
    {
        projectileShooting = transform.Find("ProjectileShootFromPosition").position;
        range = 4f;
        shootTimerMax = 1f;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //CMDebug.TextPopupMouse("Click!");
            //Debug.Log(UtilsClass.GetMouseWorldPosition());
            //ProjecTileShooting.Create(projectileShooting, UtilsClass.GetMouseWorldPosition(), towerType);
        }
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;
            EnemyNavigation enemy = GetClosestEnemy();
            if (enemy != null)
            {
                projecTileShooting.Create(projectileShooting, enemy, towerType, GameAssets.i.bulletDamage[towerType]);
            }
        }

    }
    private EnemyNavigation GetClosestEnemy()
    {
        return PointSpawner.GetClosestEnemy(transform.position, range);
    }


}
