using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Transform bossPrefab;

    private List<Type> bossAttackList;

    private void Awake()
    {
        bossAttackList = new List<Type>();
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += BossManager_OnUpgradeUnlocked;
        GameManager.Instance.OnBossBattleBegin += BossManager_OnBossBattleBegin;
    }

    private void BossManager_OnBossBattleBegin(object sender, EventArgs e)
    {
        Instantiate(bossPrefab, new Vector3(), Quaternion.identity);
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
