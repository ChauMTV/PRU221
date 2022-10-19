using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    GameObject towerPlacement;

    public LayerMask towerPlacementLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int i = FindChildIndex();
            if (i != -1)
            {
                SpawnTower(i);
            }
        }
    }
    private void SpawnTower(int i)
    {
        GameObject child = towerPlacement.transform.GetChild(i).gameObject;
        Vector3 spawnPosition = child.transform.position;
        Instantiate(GameAssets.i.tower, spawnPosition, Quaternion.identity);
        child.SetActive(false);
    }

    private int FindChildIndex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, towerPlacementLayer);
        if(hit.collider != null)
        {
            if (hit.collider.tag.Equals("TowerPlacement")){
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
