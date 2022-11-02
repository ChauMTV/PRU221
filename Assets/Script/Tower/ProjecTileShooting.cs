using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTileShooting : MonoBehaviour
{
    public void Create(Vector3 spawnPosition, Vector3 targetPosition, int tIndex,int damageAmount)
    {
        Transform projectTileTransform = Instantiate(GameAssets.i.bulletArr[tIndex], spawnPosition, Quaternion.identity);
        ProjecTileShooting projectTileShooting = projectTileTransform.GetComponent<ProjecTileShooting>();
        projectTileShooting.Setup(targetPosition, damageAmount);
    }
    private int damageAmount;
    Vector3 targetPosition;
    private void Setup(Vector3 targetPosition, int damageAmount)
    {
        this.targetPosition = targetPosition;
        this.damageAmount = damageAmount;
    }

    private void Update()
    {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            float moveSpeed = 10f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            float angle = UtilsClass.GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle);

            float destroySelfDistance = 0.1f;
            if (Vector3.Distance(transform.position, targetPosition) < destroySelfDistance)
            {
                //enemy.Damage(damageAmount);
                Destroy(gameObject);
            }
    }
}
