using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    GameObject towerPlacement;
    [SerializeField]
    GameObject towerWheel;
    [SerializeField]
    TowerWheel towerWheelBuy;


    public bool buyTower = false;
    public LayerMask towerPlacementLayer;

    public int preTowerIndex =-1;

    private void Awake()
    {
        towerWheel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!buyTower)
        {
            Hide();
        }

        if (Input.GetMouseButtonDown(0))
        {
            int placeIndex = FindChildIndex();
            if (placeIndex != -1)
            {
                preTowerIndex = placeIndex;
                Show(placeIndex);
                buyTower = true;
            }else if(placeIndex == -1 && !towerWheelBuy.towerChanged)
            {
                buyTower = false;
            }
        }
    }

    public void SpawnTower(int i, int tIndex)
    {
        GameObject child = towerPlacement.transform.GetChild(i).gameObject;
        Debug.Log(child.transform.position.x);
        Vector3 spawnPosition = child.transform.position;
        Instantiate(GameAssets.i.selectorArr[tIndex], spawnPosition, Quaternion.identity);
        child.SetActive(false);
    }

    private void Show(int i)
    {
        GameObject child = towerPlacement.transform.GetChild(i).gameObject;
        towerWheel.SetActive(true);
        towerWheel.transform.position = child.transform.position;
    }

    private void Hide()
    {
        towerWheel.SetActive(false);
    }



    private int FindChildIndex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, towerPlacementLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("TowerPlacement"))
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
