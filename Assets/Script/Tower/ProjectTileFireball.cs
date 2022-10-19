using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class ProjectTileFireball : MonoBehaviour
{
    public static void Create(Vector3 spawnPosition, Vector3 targetPosition)
    {
        Transform fireBallTransform = Instantiate(GameAssets.i.fireBall,spawnPosition,Quaternion.identity);
        ProjectTileFireball projectTileFireball = fireBallTransform.GetComponent<ProjectTileFireball>();
        projectTileFireball.Setup(targetPosition);
    }
    private Vector3 targetPosition;
    private void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed* Time.deltaTime;

        float angle = UtilsClass.GetAngleFromVectorFloat(moveDir);
        transform.eulerAngles = new Vector3(0, 0, angle);

        float destroySelfDistance = 1f;
        if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
        {
            Destroy(gameObject);
        }
    }
}
