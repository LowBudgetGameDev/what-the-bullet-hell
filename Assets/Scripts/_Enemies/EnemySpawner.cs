using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float halfSpawnLength = 35f;
    [SerializeField] private int maxEnemyAmount = 20;
    [SerializeField] private float spawnTimerMax = 1f;
    [SerializeField] private Vector2 mapSize;

    [SerializeField] private List<Transform> enemyPrefabList;
    [SerializeField] private int[] enemySpawnCompareValues;

    private List<UpgradeSO> counterUpgradesList;

    private float spawnTimer;
    private int numEnemies;
    private Vector2 cameraSize;

    private void Awake()
    {
        counterUpgradesList = new List<UpgradeSO>();

        spawnTimer = spawnTimerMax;

        Camera mainCamera = UtilsClass.GetMainCamera();
        cameraSize = new Vector2(
            mainCamera.orthographicSize * mainCamera.aspect * 2,
            mainCamera.orthographicSize * 2);
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += EnemySpawner_OnUpgradeUnlocked;
        LevelManager.Instance.OnLevelUp += EnemySpawner_OnLevelUp;
    }

    private void EnemySpawner_OnLevelUp(object sender, System.EventArgs e)
    {
        spawnTimerMax -= 0.03f;
    }

    private void EnemySpawner_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        if (e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun))
        {
            counterUpgradesList.Add(e.upgrade.counter);
        }
    }

    private void Update()
    {
        if (numEnemies >= maxEnemyAmount) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            SpawnEnemy();

            spawnTimer += spawnTimerMax;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnOffset = new Vector3(Random.Range(-halfSpawnLength, halfSpawnLength), Random.Range(-halfSpawnLength, halfSpawnLength));

        if (Mathf.Clamp(spawnOffset.x, -cameraSize.x / 2, cameraSize.x / 2) == spawnOffset.x)
        {
            spawnOffset.x = spawnOffset.x < 0 ? -cameraSize.x / 2 : cameraSize.x / 2;
        }

        if (Mathf.Clamp(spawnOffset.y, -cameraSize.y / 2, cameraSize.y / 2) == spawnOffset.y)
        {
            spawnOffset.y = spawnOffset.y < 0 ? -cameraSize.y / 2 : cameraSize.y / 2;
        }

        Vector3 spawnPosition = transform.position + spawnOffset;

        spawnPosition.x = Mathf.Clamp(spawnPosition.x, -mapSize.x / 2, mapSize.x / 2);
        spawnPosition.y = Mathf.Clamp(spawnPosition.y, -mapSize.y / 2, mapSize.y / 2);

        int randomInt = Random.Range(0, 100);

        int enemyIndex = 0;

        for (int i = 0; i < enemySpawnCompareValues.Length; i++)
        {
            if (randomInt < enemySpawnCompareValues[i])
            {
                enemyIndex = i;
                break;
            }
        }

        Transform enemy = Instantiate(enemyPrefabList[enemyIndex],
                spawnPosition,
                Quaternion.identity);

        foreach (UpgradeSO upgrade in counterUpgradesList)
        {
            enemy.gameObject.AddComponent(upgrade.GetScriptType());
        }

        enemy.GetComponent<Enemy>().SetUp(playerTransform, () => { numEnemies--; });

        numEnemies++;
    }
}
