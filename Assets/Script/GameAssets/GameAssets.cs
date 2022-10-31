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
    public GameObject towerPlacement;
    public GameObject stoneTower;
    public GameObject fireTower;
    public GameObject meteorTower;
    public GameObject clayTower;
    public GameObject[] selectorArr= new GameObject[4];

    private void Awake()
    {
        selectorArr[0] = stoneTower;
        selectorArr[1] = clayTower;
        selectorArr[2] = fireTower;
        selectorArr[3] = meteorTower;
    }
}
