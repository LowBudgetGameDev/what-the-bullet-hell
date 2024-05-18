using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float halfSpawnLength = 35f;
    [SerializeField] private int maxEnemyAmount = 20;
    [SerializeField] private float spawnTimerMax = 1f;

    [SerializeField] private Transform enemyPrefab;

    private float spawnTimer;

    private void Awake()
    {
        spawnTimer = spawnTimerMax;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            Instantiate(enemyPrefab,
                transform.position + new Vector3(Random.Range(-halfSpawnLength, halfSpawnLength), Random.Range(-halfSpawnLength, halfSpawnLength)),
                Quaternion.identity);

            spawnTimer += spawnTimerMax;
        }
    }
}
