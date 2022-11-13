using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
/// <summary>
/// Building place inspector.
/// </summary>
public class BuildingPlacesInspector : MonoBehaviour
{
    // Tower of this building place
    private GameObject myTower;
    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
       myTower = GetComponentInChildren<TowerNew>().gameObject;
        Debug.Assert(myTower , "Wrong stuff settings");
    }


    /// <summary>
    /// Chooses the tower.
    /// </summary>
    /// <returns>The tower.</returns>
    /// <param name="towerPrefab">Tower prefab.</param>
    public GameObject ChooseTower(GameObject towerPrefab)
    {
        // Destroy old tower
        if (myTower != null)
        {
            DestroyImmediate(myTower);
        }
        // Create new tower
        myTower = Instantiate(towerPrefab, transform);
        myTower.name = towerPrefab.name;
        myTower.transform.SetAsLastSibling();
        return myTower;
    }
}
#endif
