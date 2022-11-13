using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerActionsSell : TowerAction
{
    // Prefab for empty building place
    public GameObject emptyPlacePrefab;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        Debug.Assert(emptyPlacePrefab, "Wrong initial parameters");
    }

    protected override void Clicked()
    {
        // Sell the tower
        TowerNew tower = GetComponentInParent<TowerNew>();
        if (tower != null)
        {
            tower.SellTower(emptyPlacePrefab);
        }
    }
}
