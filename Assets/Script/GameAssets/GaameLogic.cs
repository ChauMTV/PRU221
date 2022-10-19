using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GaameLogic : MonoBehaviour
{
    public static void Init()
    {
    }

    private static void CreateSprite(Sprite sprite)
    {
        UtilsClass.CreateWorldSprite("Sprite", sprite, Vector3.zero, new Vector3(20, 20), 0, Color.white);
    }
}
