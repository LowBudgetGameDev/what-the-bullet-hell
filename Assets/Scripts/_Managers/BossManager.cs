using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public class OnBossSpawnedEventArgs : EventArgs
    {
        public Transform bossTransform;
    }

    public static BossManager Instance { get; private set; }

    public event EventHandler<OnBossSpawnedEventArgs> OnBossSpawned;
    public event EventHandler OnBossKilled;

    [SerializeField] private Transform bossPrefab;

    private List<Type> bossAttackList;

    private void Awake()
    {
        Instance = this;

        bossAttackList = new List<Type>();
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += BossManager_OnUpgradeUnlocked;
        GameManager.Instance.OnBossBattleBegin += BossManager_OnBossBattleBegin;
    }

    private void BossManager_OnBossBattleBegin(object sender, EventArgs e)
    {
        Transform boss = Instantiate(bossPrefab, new Vector3(), Quaternion.identity);

        foreach (Type bossAttack in bossAttackList)
        {
            boss.gameObject.AddComponent(bossAttack);
        }

        boss.GetComponent<Boss>().SetUp();

        boss.GetComponent<HealthSystem>().OnDied += (object sender, EventArgs e) =>
        {
            OnBossKilled?.Invoke(this, EventArgs.Empty);
        };

        OnBossSpawned?.Invoke(this, new OnBossSpawnedEventArgs() { bossTransform = boss });
    }

    private void BossManager_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        switch (e.upgrade.upgrade)
        {
            case Upgrade.Health:
                bossAttackList.Add(typeof(SpikeAttack));
                break;
            case Upgrade.Fire_Rate:
                bossAttackList.Add(typeof(LazerAttack));
                break;
            case Upgrade.Life_Steal:
                bossAttackList.Add(typeof(HealAttack));
                break;
            case Upgrade.Explosive_Bullets:
                bossAttackList.Add(typeof(BombAttack));
                break;
            case Upgrade.Poison:
                bossAttackList.Add(typeof(GasAttack));
                break;
            case Upgrade.Piercing_Bullets:
                bossAttackList.Add(typeof(HomingMissileAttack));
                break;
            case Upgrade.Large_Bullets:
                bossAttackList.Add(typeof(HugeBulletAttack));
                break;
            case Upgrade.Shotgun:
                bossAttackList.Add(typeof(ManyBulletsAttack));
                break;
        }
    }
}
