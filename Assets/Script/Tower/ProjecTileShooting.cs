using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTileShooting : MonoBehaviour
{
    public void Create(Vector3 spawnPosition, GameObject enemy, int tIndex,int damageAmount)
    {
        Transform projectTileTransform = Instantiate(GameAssets.i.bulletArr[tIndex], spawnPosition, Quaternion.identity);
        ProjecTileShooting projectTileShooting = projectTileTransform.GetComponent<ProjecTileShooting>();
        projectTileShooting.Setup(enemy, damageAmount);
    }
    private int damageAmount;
    private GameObject enemy;
    private void Setup(GameObject enemy, int damageAmount)
    {
        this.enemy = enemy;
        this.damageAmount = damageAmount;
    }
    private void Start()
    {
        //enemy = GameObject.FindGameObjectsWithTag("Enemy")[];
        //enemy = GameAssets.i.enemies[targetPosition];
    }

    private void Update()
    {
        if (enemy!= null)
        {
            Vector3 moveDir = (enemy.transform.position - transform.position).normalized;
            float moveSpeed = 10f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            float angle = UtilsClass.GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle);

            float destroySelfDistance = 0.1f;
            if (Vector3.Distance(transform.position, enemy.transform.position) < destroySelfDistance)
            {
                enemy.GetComponent<EnemyNavigation>().Damage(damageAmount);
                Destroy(transform.gameObject);
            }
        }

    }
}
