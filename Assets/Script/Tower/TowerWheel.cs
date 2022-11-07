using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWheel : MonoBehaviour
{

    public LayerMask towerWheelLayer;
    public int towerIndex;
    [SerializeField]
    GameObject towerWheel;

    [SerializeField]
    TowerPlacement towerPlacement;

    public int preTowerType = -1;


    public bool towerChanged = false;
    void Update()
    {

        if (Input.GetMouseButtonDown(0) )
        {
            towerIndex = FindChildIndex();
            if (towerIndex != -1)
            {
                preTowerType = towerIndex;
                towerPlacement.SpawnTower(towerPlacement.preTowerIndex, towerIndex);
                towerPlacement.buyTower = false;
                towerChanged = false;
            }
        }
        
    }

    
    private int FindChildIndex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, towerWheelLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("TowerWheel"))
            {
                return hit.collider.transform.parent.GetSiblingIndex();
            }
            else
            {
                return -1;
            }
        }
        return -1;
    }
}
