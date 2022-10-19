using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Tower : MonoBehaviour
{
    private Vector2 projectileShooting;
    private void Awake()
    {
        projectileShooting = transform.Find("ProjectileShooting").position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //CMDebug.TextPopupMouse("Click!");
            //ProjectTileFireball.Create(projectileShooting,UtilsClass.GetMouseWorldPosition());
        }
    }
}
