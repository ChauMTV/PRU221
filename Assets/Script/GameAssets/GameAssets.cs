using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }
    public Transform fireBall;
    public Transform stoneB;
    public Transform clayB;
    public Transform meteorB;
    public GameObject towerPlacement;
    public GameObject stoneTower;
    public GameObject fireTower;
    public GameObject meteorTower;
    public GameObject clayTower;
    public GameObject[] selectorArr= new GameObject[4];
    public Transform[] bulletArr= new Transform[4];
    public EnemyNavigation[] enemies = new EnemyNavigation[4];
    public int sDame;
    public int cDame;
    public int fDame;
    public int mDame;
    public int[] bulletDamage = new int[4];

    private void Awake()
    {
        selectorArr[0] = stoneTower;
        selectorArr[1] = clayTower;
        selectorArr[2] = fireTower;
        selectorArr[3] = meteorTower;
        bulletArr[0] = stoneB;
        bulletArr[1] = clayB;
        bulletArr[2] = fireBall;
        bulletArr[3] = meteorB;
        bulletDamage[0] = sDame;
        bulletDamage[1] = cDame;
        bulletDamage[2] = fDame;
        bulletDamage[3] = mDame;
    }

}
